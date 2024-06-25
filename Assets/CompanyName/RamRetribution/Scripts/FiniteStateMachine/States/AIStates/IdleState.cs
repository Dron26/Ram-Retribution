using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.AIStates
{
    public class IdleState : BaseState
    {
        public override void Enter()
        {
            Debug.Log($"Entered idle state");
        }

        public override void Exit()
        {
            Debug.Log($"Exit idle state");
        }
    }
}