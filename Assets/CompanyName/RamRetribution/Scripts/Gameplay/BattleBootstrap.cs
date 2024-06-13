using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleBootstrap
    {
        private readonly GameData _gameData;
        private readonly List<string> _selectedRamsId;
        private readonly LeaderDataState _leaderData;

        private UnitSpawner _unitSpawner;

        public BattleBootstrap(GameData gameData, List<string> selectedRamsId, LeaderDataState leaderData)
        {
            _gameData = gameData;
            _selectedRamsId = selectedRamsId;
            _leaderData = leaderData;
        }

        public void Init(BattleCommander battleCommander)
        {
            CreateSpawner();
            CreateLeader();
            CreateRams();

            battleCommander.Init(_unitSpawner);
        }

        private void CreateSpawner()
        {
            var unitFactory = new UnitFactory();

            _unitSpawner = Object.Instantiate(
                Services
                    .ResourceLoadService
                    .Load<UnitSpawner>($"{AssetPaths.CommonPrefabs}{nameof(UnitSpawner)}"));

            var unitConfigs = Services
                .ResourceLoadService
                .Load<ConfigsContainer>($"{AssetPaths.Configs}{nameof(ConfigsContainer)}");
            
            _unitSpawner.AddPlacementStrategy(UnitTypes.Ram, new CirclePlacementStrategy(0.25f, 0.5f));
            _unitSpawner.AddPlacementStrategy(UnitTypes.Enemy, new CirclePlacementStrategy(0.5f, 1f));
            _unitSpawner.Init(unitFactory, unitConfigs);
        }

        private void CreateLeader()
        {
            _unitSpawner.Spawn(_leaderData.Config.Id);
        }

        private void CreateRams()
        {
            foreach (var config in _selectedRamsId)
                _unitSpawner.Spawn(config);
        }
    }
}