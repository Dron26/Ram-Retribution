using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild.ReworkLevelBuild_GridConfigurator;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class LevelBuilder
    {
        private readonly GridConfigurator _gridConfigurator;

        private Level _currentLevel;
        private Vector3 _startBuildPosition;

        public LevelBuilder(GridConfigurator gridConfigurator)
        {
            _gridConfigurator = gridConfigurator;
            _startBuildPosition = Vector3.zero;
        }

        public async UniTask<Level> Build(int levelNumber)
        {
            var grid = await _gridConfigurator.CreateAsync(levelNumber, _startBuildPosition);

            _currentLevel = new Level(levelNumber, grid);
            
            _startBuildPosition = Vector3.zero
                .With(z: GridConstants.SizeY * GridConstants.StepBetweenTiles);

            return _currentLevel;
        }
    }
}