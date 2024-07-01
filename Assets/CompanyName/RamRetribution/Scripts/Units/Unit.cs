using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    [SelectionBase]
    [RequireComponent(typeof(AIMovement))]
    public abstract class Unit : MonoBehaviour, IAttackable
    {
        public readonly List<Unit> CurrentEnemies = new();

        private IAttackComponent _attackComponent;
        private AIMovement _aiMovement;
        private Animator _animator;
        private CancellationTokenSource _cancellationToken;

        private PriorityTypes _currentTargetPriorityType;

        public event Action<Unit> Fleeing;
        public event Action<List<Unit>> MyAttackersWaitingCommand;
        public IDamageable Damageable { get; private set; }
        public Transform SelfTransform { get; private set; }
        public int Damage => _attackComponent.Damage;
        public abstract UnitTypes Type { get; }
        public PriorityTypes Priority { get; private set; }
        public bool IsActive { get; private set; }

        public void Init(IDamageable health, IAttackComponent attackComponent, PriorityTypes priority)
        {
            _aiMovement = GetComponent<AIMovement>();
            _animator = GetComponentInChildren<Animator>();
            SelfTransform = transform;

            Damageable = health;
            _attackComponent = attackComponent;

            IsActive = false;
            Priority = priority;

            Damageable.HealthEnded += OnHealthEnded;
            _cancellationToken = new CancellationTokenSource();
        }

        #region BattleActions

        public void MoveToPoint(Vector3 destination, Action callback = null)
        {
            _aiMovement.Move(destination, callback);
        }

        public void MoveTowards(Transform target, Action callback = null)
        {
            _aiMovement.MoveTowards(target).OnComplete(callback);
        }

        public void AttackGate(Gate gate)
        {
            _ = Attack(gate);
        }

        private async UniTask Attack(IAttackable target)
        {
            if(!target.IsActive)
                Debug.Log($"target ne active");
            
            while (target.IsActive)
            {
                if (CanAttack(target.SelfTransform))
                {
                    _attackComponent.Attack(target.Damageable);
                    _animator.SetTrigger(AIAnimatorParams.Attack);

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(_attackComponent.AttackSpeed),
                        DelayType.DeltaTime,
                        PlayerLoopTiming.Update,
                        _cancellationToken.Token);
                }
                else
                {
                    MoveTowards(target.SelfTransform);

                    await UniTask.Yield(PlayerLoopTiming.Update, _cancellationToken.Token);
                }
            }
        }

        public void Heal(int amount)
        {
            Damageable.Restore(amount);
        }

        public void FindTarget(Dictionary<int, List<Unit>> targetsByPriority)
        {
            //CancelToken();

            if (_currentTargetPriorityType == PriorityTypes.High)
            {
                GetEnemyToAttack(targetsByPriority, PriorityTypes.High, PriorityTypes.Medium, PriorityTypes.Small);
                return;
            }

            switch (_currentTargetPriorityType)
            {
                case PriorityTypes.Leader:
                    GetEnemyToAttack(targetsByPriority, PriorityTypes.High, PriorityTypes.Medium, PriorityTypes.Small);
                    break;
                case PriorityTypes.Small:
                    GetEnemyToAttack(targetsByPriority, PriorityTypes.High, PriorityTypes.Medium);
                    break;
                case PriorityTypes.Medium:
                    GetEnemyToAttack(targetsByPriority, PriorityTypes.High);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetEnemyToAttack(Dictionary<int, List<Unit>> targetsByPriority,
            params PriorityTypes[] priorityTypesArray)
        {
            foreach (var priority in priorityTypesArray)
            {
                if (targetsByPriority[(int)priority].Count > 0)
                {
                    var unitWithFewerAttackers = targetsByPriority[(int)priority]
                        .OrderBy(unit => unit.CurrentEnemies.Count)
                        .First();

                    _currentTargetPriorityType = unitWithFewerAttackers.Priority;
                    Fleeing += unitWithFewerAttackers.OnAttackersFleeing;
                    unitWithFewerAttackers.CurrentEnemies.Add(this);
                    Attack(unitWithFewerAttackers).Forget();

                    return;
                }
            }
        }

        #endregion

        public void ActivateAgent()
        {
            IsActive = true;
            _aiMovement.ActivateNavMesh();
        }

        public void SelfDestroy()
        {
            gameObject.SetActive(false);
        }

        public abstract void Accept(IUnitVisitor visitor);

        private bool CanAttack(Transform target)
        {
            return (target.transform.position - SelfTransform.position).sqrMagnitude <= _attackComponent.Distance;
        }

        private void OnHealthEnded()
        {
            CancelToken();
            Damageable.HealthEnded -= OnHealthEnded;
            IsActive = false;
            Fleeing?.Invoke(this);
            MyAttackersWaitingCommand?.Invoke(CurrentEnemies);

            SelfDestroy();
        }

        private void CancelToken()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        private void OnAttackersFleeing(Unit unit)
            => CurrentEnemies.Remove(unit);
    }
}