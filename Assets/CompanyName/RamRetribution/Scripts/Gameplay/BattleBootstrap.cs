using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts._TestScripts;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Gameplay.Level;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleBootstrap
    {
        private readonly GameData _gameData;
        private readonly List<string> _selectedRamsId;
        private readonly LeaderDataState _leaderData;

        private UnitSpawner _unitSpawner;
        private LevelBuilder _levelBuilder;
        private Squad _ramsSquad;

        public BattleBootstrap(GameData gameData, List<string> selectedRamsId, LeaderDataState leaderData)
        {
            _gameData = gameData;
            _selectedRamsId = selectedRamsId;
            _leaderData = leaderData;
        }

        public void Init()
        {
            CreateSpawner();
            CreateLevelBuilder();
            CreateBattleMediator();

            CreateTestCombat();
        }

        private void CreateSpawner()
        {
            _unitSpawner = Object.Instantiate(
                Services
                    .ResourceLoadService
                    .Load<UnitSpawner>($"{AssetPaths.CommonPrefabs}{nameof(UnitSpawner)}"));

            var unitConfigs = Services
                .ResourceLoadService
                .Load<ConfigsContainer>($"{AssetPaths.Configs}{nameof(ConfigsContainer)}");

            var unitFactory = new UnitFactory(unitConfigs);
            
            _unitSpawner.Init(unitFactory, _leaderData, _selectedRamsId);
        }

        private void CreateLevelBuilder()
        {
            _levelBuilder = Object.Instantiate(Services
                .ResourceLoadService
                .Load<LevelBuilder>($"{AssetPaths.CommonPrefabs}{nameof(LevelBuilder)}"));
            
            _levelBuilder.Init();
            _levelBuilder.SetLevelConfiguration(new LevelConfigurator());
        }

        private void CreateBattleMediator()
        {
            var battleMediator = new BattleMediator();
            battleMediator.RegisterSpawner(_unitSpawner);
        }

        private void CreateTestCombat()
        {
            TestCombat testCombatPrefab = Services
                .ResourceLoadService
                .Load<TestCombat>($"{AssetPaths.CommonPrefabs}{nameof(TestCombat)}");

            Object.Instantiate(testCombatPrefab).Init(_levelBuilder, _unitSpawner);
        }
    }
}