using System;
using System.Collections.Generic;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    [SelectionBase]
    [RequireComponent(typeof(Animator))]
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
        public PriorityTypes Priority { get; private set; }
        public bool IsActive { get; private set; }

        public void Init(IDamageable health, IAttackComponent attackComponent, PriorityTypes priority)
        {
            _aiMovement = GetComponent<AIMovement>();
            _animator = GetComponent<Animator>();
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

        public async UniTask MoveNavMeshAsync(Transform target) 
            => await _aiMovement.NavMeshMoveAsync(target, _cancellationToken.Token);
        
        public async UniTask<bool> TransformMoveToPointAsync(Vector3 destination, CancellationToken otherToken = default,
            Action callback = null)
        {
            return await _aiMovement.TransformMoveToPointAsync(destination, otherToken, callback);
        }

        public async UniTaskVoid Attack(IAttackble target)
        {
            while (target.IsActive)
            {
                if (CanAttack(target.SelfTransform))
                {
                    var lookDirection = (target.SelfTransform.position - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(lookDirection);

                    AttackComponent.Attack(target);
                    var attackInterval = 1f / AttackComponent.AttackSpeed;

                    await UniTask.Delay(
                        TimeSpan.FromSeconds(attackInterval),
                        DelayType.Realtime,
                        PlayerLoopTiming.Update,
                        _cancellationToken.Token);
                }
                else
                {
                    await MoveNavMeshAsync(target.SelfTransform);
                }
            }
        }

        #endregion

        public async UniTaskVoid FleeAsync(Vector3 to)
        {
            var task = await TransformMoveToPointAsync(to);
            
            if(task)
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

        private bool CanAttack(Transform target) 
            => (target.transform.position - SelfTransform.position).sqrMagnitude <= AttackComponent.Distance;

        protected virtual void OnHealthEnded(IDamageable damageable)
        {
            CancelToken();
            damageable.HealthEnded -= OnHealthEnded;
            IsActive = false;
            Fleeing?.Invoke(this);
        }

        protected void CancelToken()
        {
            _cancellationToken?.Cancel();
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        public void OnAttackersFleeing(Unit unit)
            => CurrentEnemies.Remove(unit);
    }
}