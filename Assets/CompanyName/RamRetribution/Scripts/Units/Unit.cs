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

        private AIMovement _aiMovement;
        private Animator _animator;
        private CancellationTokenSource _cancellationToken;

        public event Action<Unit> Fleeing;
        public IAttackComponent AttackComponent { get; private set; }
        public IDamageable Damageable { get; private set; }
        public Transform SelfTransform { get; private set; }
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
            AttackComponent = attackComponent;

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
        
        public async UniTask Attack(IAttackble target, Transform pointForAttack = null)
        {
            while (target.IsActive)
            {
                if (CanAttack(target.SelfTransform))
                {
                    var lookDirection = (target.SelfTransform.position - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(lookDirection);
                    
                    AttackComponent.Attack(target.Damageable);

                    _animator.SetInteger(AIAnimatorParams.Attack,
                        Type == UnitTypes.Ram
                            ? Random.Range(0, AIAnimatorParams.RamsAttackAnimationCount)
                            : Random.Range(0, AIAnimatorParams.EnemyAttackAnimationCount));

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(AttackComponent.AttackSpeed),
                        DelayType.Realtime,
                        PlayerLoopTiming.Update,
                        _cancellationToken.Token);
                }
                else
                {
                    var targetToMove = pointForAttack == null
                        ? target.SelfTransform
                        : pointForAttack;

                    await MoveTowardsAsync(targetToMove);
                }
            }
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

        public void Flee(Vector3 to)
        {
            DeactivateAgent();
            MoveToPoint(to, () => gameObject.SetActive(false));
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
            return (target.transform.position - SelfTransform.position).sqrMagnitude <= AttackComponent.Distance;
        }

        private async UniTask MoveTowardsAsync(Transform target)
        {
            await _aiMovement.MoveTowards(target, _cancellationToken.Token);
        }
        
        private void OnHealthEnded()
        {
            CancelToken();
            Damageable.HealthEnded -= OnHealthEnded;
            IsActive = false;
            Fleeing?.Invoke(this);
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