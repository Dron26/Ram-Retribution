using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.AIStates
{
    public class AttackState : BaseState
    {
        private readonly Animator _animator;
        
        public AttackState(Animator animator)
        {
            _animator = animator;
        }
        
        public override void Enter()
        {
            Debug.Log($"Entered Attack");
            _animator.CrossFade(AIAnimatorParams.Attack, 0.5f);
        }
    }
}