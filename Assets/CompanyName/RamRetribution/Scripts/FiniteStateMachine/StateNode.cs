using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.Transitions;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine
{
    public class StateNode
    {
        public StateNode(IState state)
        {
             State = state;
             Transitions = new HashSet<ITransition>();
        }

        public IState State { get; }
        public HashSet<ITransition> Transitions { get; } 

        public void AddTransition(IState toState, IPredicate condition)
            => Transitions.Add(new Transition(toState, condition));
    }
}