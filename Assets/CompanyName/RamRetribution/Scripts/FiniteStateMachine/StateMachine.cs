using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;

        public StateMachine()
        {
            AddState(new LobbyBootstrapState(this));
            AddState(new GameBootstrapState(this));
        }
        
        public void SetState<TState>()
            where TState : IState
        {
            var type = typeof(TState);

            _currentState?.Exit();
            _currentState = _states[type];
            _currentState.Enter();
        }

        private void AddState(IState state)
        {
            var type = state.GetType();
            _states.Add(type, state);
        }
    }
}