using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates
{
    public class GateBattleState : BaseState
    {
        private readonly LevelBuilder _levelBuilder;
        private readonly SquadsHolder _squadsHolder;
        private Gate _gate;
        
        public GateBattleState(LevelBuilder levelBuilder, SquadsHolder squadsHolder)
        {
            _levelBuilder = levelBuilder;
            _squadsHolder = squadsHolder;
        }
        
        public override void Enter()
        {
            Debug.Log($"Entered GateBattle");
            _gate = _levelBuilder.CurrentGate;
        }

        public override void Update(float deltaTime)
        {
            _squadsHolder.Rams.Attack(_gate);
        }
    }
}