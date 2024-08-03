using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using Object = UnityEngine.Object;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleBootstrap
    {
        private readonly ModulesContainer _modulesContainer;
        private readonly List<ConfigId> _selectedRamsId;
        private readonly LeaderDataState _leaderData;

        private UnitSpawner _unitSpawner;

        public BattleBootstrap(
            ModulesContainer modulesContainer,
            List<ConfigId> selectedRamsId,
            LeaderDataState leaderData)
        {
            _modulesContainer = modulesContainer;
            _selectedRamsId = selectedRamsId;
            _leaderData = leaderData;
        }

        public void Init()
        {
            CreateSpawner();
            CreateBattleMediator();
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
            _modulesContainer.Register(_unitSpawner);
        }

        private void CreateBattleMediator()
        {
            var battleMediator = new BattleMediator();
            battleMediator.RegisterSpawner(_unitSpawner);
            _modulesContainer.Register(battleMediator);
        }
    }
}