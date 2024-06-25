using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.Transitions;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, StateNode> _nodes = new Dictionary<Type, StateNode>();
        private readonly HashSet<ITransition> _anyTransitions = new HashSet<ITransition>();
        private StateNode _currentNode;

        public void SetState<TState>()
            where TState : IState
        {
            var type = typeof(TState);

            _currentNode?.State.Exit();
            _currentNode = _nodes[type];
            _currentNode.State.Enter();
        }

        public void Update(float deltaTime)
        {
            ITransition transition = GetTransition();

            if (transition != null)
                ChangeState(transition.ToState);

            _currentNode.State?.Update(deltaTime);
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
            => GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);

        public void AddAnyTransition(IState to, IPredicate condition)
            => _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));

        private StateNode GetOrAddNode(IState state)
        {
            StateNode stateNode = _nodes.GetValueOrDefault(state.GetType());

            if (stateNode == null)
            {
                stateNode = new StateNode(state);
                _nodes.Add(state.GetType(), stateNode);
            }

            return stateNode;
        }

        private void ChangeState(IState state)
        {
            if (state == _currentNode.State)
                return;

            IState nextState = _nodes[state.GetType()].State;

            _currentNode.State?.Exit();
            nextState?.Enter();
            _currentNode = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition != null)
                    if (transition.Condition.Evaluate())
                        return transition;

            foreach (var transition in _currentNode.Transitions)
                if (transition.Condition != null)
                    if (transition.Condition.Evaluate())
                        return transition;

            return null;
        }
    }
}