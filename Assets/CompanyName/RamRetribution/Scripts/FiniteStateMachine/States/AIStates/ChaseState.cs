using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.AIStates
{
    public class ChaseState : BaseState
    {
        private readonly Transform _target;
        private readonly Animator _animator;
        private readonly AIMovement _aiMovement;

        public ChaseState(Animator animator, AIMovement aiMovement, Transform target)
        {
            _target = target;
            _animator = animator;
            _aiMovement = aiMovement;
        }

        public override void Enter()
        {
            Debug.Log($"Entered Chase");
            _animator.CrossFade(AIAnimatorParams.Run, 0.3f);
        }

        public override void Update(float deltaTime)
        {
            _aiMovement.MoveTowards(_target.transform);
        }
    }
}