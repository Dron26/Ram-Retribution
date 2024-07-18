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
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Units
{
    [SelectionBase]
    [RequireComponent(typeof(AIMovement))]
    public abstract class Unit : MonoBehaviour, IAttackble
    {
        public readonly List<Unit> CurrentEnemies = new();

        private IAttackComponent _attackComponent;
        private AIMovement _aiMovement;
        private Animator _animator;
        private CancellationTokenSource _cancellationToken;

        public event Action<Unit> Fleeing;
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
            _aiMovement.Init(_animator);
            
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

        private async UniTask MoveTowards(Transform target, Action callback = null)
        {
            _aiMovement.OnComplete(callback);
            
            await _aiMovement.MoveTowards(target, _cancellationToken.Token);
        }

        public async UniTask Attack(IAttackble target)
        {
            while (target.IsActive)
            {
                if (CanAttack(target.SelfTransform))
                {
                    _attackComponent.Attack(target.Damageable);
                    _animator.SetInteger(AIAnimatorParams.Attack, Random.Range(1,3));

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(_attackComponent.AttackSpeed),
                        DelayType.DeltaTime,
                        PlayerLoopTiming.Update,
                        _cancellationToken.Token);
                }
                else
                {
                    await MoveTowards(target.SelfTransform);
                }
            }
        }

        public void Heal(int amount)
        {
            Damageable.Restore(amount);
        }

        public void NotifyFindTarget(Dictionary<int, List<Unit>> targetsByPriority)
        {
            CancelToken();

            FindTarget(
                targetsByPriority,
                PriorityTypes.High,
                PriorityTypes.Medium,
                PriorityTypes.Small,
                PriorityTypes.Leader);
        }

        private void FindTarget(Dictionary<int, List<Unit>> targetsByPriority,
            params PriorityTypes[] priorityTypesArray)
        {
            foreach (var priority in priorityTypesArray)
            {
                if (targetsByPriority[(int)priority].Count > 0)
                {
                    var unitWithFewerAttackers = targetsByPriority[(int)priority]
                        .OrderBy(unit => unit.CurrentEnemies.Count)
                        .First();

                    Fleeing += unitWithFewerAttackers.OnAttackersFleeing;
                    unitWithFewerAttackers.CurrentEnemies.Add(this);
                    Attack(unitWithFewerAttackers).Forget();

                    return;
                }
            }
        }

        #endregion

        public void Flee()
        {
            gameObject.SetActive(false);
        }
        
        public void ActivateAgent()
        {
            IsActive = true;
            _aiMovement.ActivateNavMesh();
        }

        public void DeactivateAgent()
        {
            IsActive = false;
            _aiMovement.DeactivateNavMesh();
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

            gameObject.SetActive(false);
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