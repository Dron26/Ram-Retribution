using System;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    [SelectionBase]
    [RequireComponent(typeof(AIMovement))]
    public abstract class Unit : MonoBehaviour
    {
        private IDamageable _health;
        private IAttackComponent _attackComponent;
        private AIMovement _aiMovement;
        private Animator _animator;
        private Transform _selfTransform;
        
        private CooldownTimer _attackTimer;
        
        public event Action<Unit> Fleeing;
        public IDamageable Damageable => _health;
        public int Damage => _attackComponent.Damage;
        public abstract UnitTypes Type { get; }
        public int Priority { get; private set; }
        public bool IsActive { get; private set; }
        
        private void Update()
        {
            _attackTimer?.Tick(Time.deltaTime);
        }
        
        public void Init(IDamageable health, IAttackComponent attackComponent, int priority)
        {
            _aiMovement = GetComponent<AIMovement>();
            _animator = GetComponentInChildren<Animator>();
            _selfTransform = transform;
            
            _health = health;
            _attackComponent = attackComponent;
            
            IsActive = false;
            Priority = priority;
            
            _attackTimer = new CooldownTimer(_attackComponent.AttackSpeed);

            _health.HealthEnded += OnHealthEnded;
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
        
        public void Attack(IDamageable damageable)
        {
            if(_attackTimer.IsRunning)
                return;
            
            _attackTimer.Start();
            _attackComponent.Attack(damageable);
            _animator.SetTrigger(AIAnimatorParams.Attack);
        }

        public virtual void Heal(int amount)
        {
            _health.Restore(amount);
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
            Destroy(gameObject);
        }
        
        public abstract void Accept(IUnitVisitor visitor);

        private void OnHealthEnded()
        {
            _health.HealthEnded -= OnHealthEnded;
            Fleeing?.Invoke(this);
        }
    }
}