using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.AIStates
{
    public class AttackState : BaseState
    {
        private readonly Unit _target;
        private readonly Animator _animator;
        
        public AttackState(Animator animator)
        {
            _animator = animator;
        }
        
        public override void Enter()
        {
            _animator.CrossFade(AIAnimatorParams.Attack, 0.5f);
        }

        public override void Update()
        {
            Debug.Log($"In state {nameof(AttackState)}");
        }
    }
}