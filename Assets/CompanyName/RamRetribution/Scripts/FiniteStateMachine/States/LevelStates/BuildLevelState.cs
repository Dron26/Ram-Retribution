using CompanyName.RamRetribution.Scripts.Gameplay;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates
{
    public class BuildLevelState : BaseState
    {
        private readonly LevelBuilder _levelBuilder;
        
        public BuildLevelState(LevelBuilder levelBuilder)
        {
            _levelBuilder = levelBuilder;
        }
        
        public override void Enter()
        {
            _levelBuilder.Build();
        }
    }
}