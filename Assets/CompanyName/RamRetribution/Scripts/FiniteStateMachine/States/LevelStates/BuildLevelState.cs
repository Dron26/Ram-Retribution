using CompanyName.RamRetribution.Scripts.Gameplay;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates
{
    public class BuildLevelState : BaseState
    {
        private readonly LevelBuilder _levelBuilder;
        private readonly UnitSpawner _unitSpawner;
        private readonly Transform _ramsStartPoint;
        
        public BuildLevelState(LevelBuilder levelBuilder, UnitSpawner spawner, Transform ramsStartPoint)
        {
            _levelBuilder = levelBuilder;
            _unitSpawner = spawner;
            _ramsStartPoint = ramsStartPoint;
        }
        
        public override void Enter()
        {
            _levelBuilder.Build();
            _unitSpawner.Squads.Rams.SetOrigin(_ramsStartPoint);
        }
    }
}