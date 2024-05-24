using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States
{
    public class GameLoopState : BaseState
    {
        private readonly StateMachine _stateMachine;

        public GameLoopState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}