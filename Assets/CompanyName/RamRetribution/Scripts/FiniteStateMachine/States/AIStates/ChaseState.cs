using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.AIStates
{
    public class ChaseState : BaseState
    {
        //private readonly Transform _target;
        private readonly Animator _animator;
        private readonly AIMovement _aiMovement;

        public ChaseState(Animator animator, AIMovement aiMovement)
        {
            //_target = target;
            _animator = animator;
            _aiMovement = aiMovement;
        }

        public override void Enter()
        {
            _animator.CrossFade(AIAnimatorParams.Run, 0.3f);
        }

        public override void Update()
        {
            //_aiMovement.Move(_target.transform.position);
        }
    }
}