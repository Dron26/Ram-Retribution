namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States
{
    public class BootstrapState : BaseState
    {
        public override void Enter()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            
#endif
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}