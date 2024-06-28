using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompanyName.RamRetribution.Scripts.Units
{
    [SelectionBase]
    [RequireComponent(typeof(AIMovement))]
    public abstract class Unit : MonoBehaviour
    {
        public List<Unit> CurrentEnemies = new();

        private IDamageable _health;
        private IAttackComponent _attackComponent;
        private AIMovement _aiMovement;
        private Animator _animator;
        private Transform _selfTransform;
        private CancellationTokenSource _cancellationToken;

        private PriorityTypes _currentTargetPriorityType;
        private CooldownTimer _attackTimer;

        public event Action<Unit> Fleeing;
        public event Action<List<Unit>> MyAttackerWaitingCommand;
        public IDamageable Damageable => _health;
        public int Damage => _attackComponent.Damage;
        public abstract UnitTypes Type { get; }
        public PriorityTypes Priority { get; private set; }
        public bool IsActive { get; private set; }

        public void Init(IDamageable health, IAttackComponent attackComponent, PriorityTypes priority)
        {
            _aiMovement = GetComponent<AIMovement>();
            _animator = GetComponentInChildren<Animator>();
            _selfTransform = transform;

            _health = health;
            _attackComponent = attackComponent;

            IsActive = false;
            Priority = priority;

            _health.HealthEnded += OnHealthEnded;
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

        private async UniTask Attack(Unit unit)
        {
            var target = unit;

            //пока таргет жив = атакуем. При смене таргета заверщаем цикл и вызываем Attack()
            while (target.IsActive)
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(_attackComponent.AttackSpeed),
                    DelayType.DeltaTime,
                    PlayerLoopTiming.Update,
                    _cancellationToken.Token);

                if (CanAttack(target.transform))
                {
                    _attackComponent.Attack(target.Damageable);
                    _animator.SetTrigger(AIAnimatorParams.Attack);
                }
                else
                {
                    MoveTowards(target.transform);
                }
            }
        }

        public void Heal(int amount)
        {
            _health.Restore(amount);
        }

        public void FindTarget(Dictionary<int, List<Unit>> targetsByPriority)
        {
            CancelToken();
            
            if (_currentTargetPriorityType == PriorityTypes.High)
            {
                GetEnemyToAttack(targetsByPriority, PriorityTypes.High);
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
                    Attack(unitWithFewerAttackers);

                    return;
                }
            }
        }

        #endregion

        public bool CanAttack(Transform target)
        {
            return (target.transform.position - _selfTransform.position).sqrMagnitude <= _attackComponent.Distance;
        }

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

        private void OnHealthEnded()
        {
            _health.HealthEnded -= OnHealthEnded;
            IsActive = false;
            Fleeing?.Invoke(this);
            CancelToken();
            MyAttackerWaitingCommand?.Invoke(CurrentEnemies);

            SelfDestroy();
        }

        private void CancelToken()
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        private void OnAttackersFleeing(Unit unit)
            => CurrentEnemies.Remove(unit);
    }
}