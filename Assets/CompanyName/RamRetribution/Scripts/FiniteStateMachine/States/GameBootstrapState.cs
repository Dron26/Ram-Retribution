using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States
{
    public class GameBootstrapState : BaseState
    {
        private readonly StateMachine _stateMachine;

        public GameBootstrapState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public override void Enter()
        {
            Services.Init();
            _stateMachine.SetState(new GameLoopState(_stateMachine));
        }
    }
}