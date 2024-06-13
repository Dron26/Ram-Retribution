using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.Predicates;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.AIStates;
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
        private StateMachine _stateMachine;

        public abstract UnitTypes Type { get; }   
        
        // private void Update()
        // {
        //     _stateMachine.Update();
        // }

        public void Init(IDamageable health, IAttackComponent attackComponent)
        {
            _health = health;
            _attackComponent = attackComponent;
            
            _aiMovement = GetComponent<AIMovement>();
            _animator = GetComponentInChildren<Animator>();
            
            // _stateMachine = new StateMachine();
            //
            // var chaseState = new ChaseState(_animator, _aiMovement);
            // var attackState = new AttackState(_animator);
            //
            // At(chaseState,attackState, new FuncPredicate(IsCloseToTarget));
            // At(attackState, chaseState, new FuncPredicate(IsFarFromTarget));
            //
            // _stateMachine.SetState<ChaseState>();
        }

        public virtual void Move(Vector3 destination, Action callback = null)
        {
            _aiMovement.Move(destination).OnComplete(callback);
        }

        public virtual void Attack(IDamageable damageable)
        {
            _attackComponent.Attack(damageable);
        }
        
        public virtual void TakeDamage(AttackType type, int amount)
        {
            _health.TakeDamage(type, amount);
        }

        public virtual void Heal(int amount)
        {
            _health.Heal(amount);
        }
        
        public abstract void Accept(IUnitVisitor visitor);
        
        private void At(IState fromState, IState toState, IPredicate condition)
            => _stateMachine.AddTransition(fromState, toState, condition);
        
        private void Any(IState toState, IPredicate condition)
            => _stateMachine.AddAnyTransition(toState, condition);
    }
}