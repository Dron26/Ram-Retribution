using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.Transitions
{
    public class Transition : ITransition
    {
        public Transition(IState toState, IPredicate condition)
        {
            ToState = toState;
            Condition = condition;
        }

        public IState ToState { get; }
        public IPredicate Condition { get; }
    }
}