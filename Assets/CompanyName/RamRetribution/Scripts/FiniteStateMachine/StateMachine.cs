using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;

        public StateMachine(ISaveLoadDataService saveLoadDataService, GameData gameData, ShopDataState shopDataState, LeaderDataState leaderDataState)
        {
            AddState(new LobbyBootstrapState(saveLoadDataService, gameData, shopDataState));
            AddState(new GameBootstrapState(saveLoadDataService, gameData));
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