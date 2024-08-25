using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class LobbyBootstrapState : IState
    {
        private readonly GameData _gameData;
        private readonly ShopDataState _shopData;
        private readonly IDataService _dataService;

        public LobbyBootstrapState(GameData gameData, ShopDataState shopData,
            IDataService dataService)
        {
            _gameData = gameData;
            _shopData = shopData;
            _dataService = dataService;
        }

        public void Enter()
        {
            SceneManager.LoadSceneAsync(SceneNames.Lobby);
        }

        public void Exit()
        {
            _dataService.Save(_gameData);
            _dataService.Save(_shopData);
        }

        private void InitSpells()
        {
            var spellsContainer = Services
                .ResourceLoadService
                .Load<SpellsContainer>($"{AssetPaths.Configs}{nameof(SpellsContainer)}");

            var spellsFactory = new SpellsFactory(spellsContainer);
            var selectedSpells = new List<ISpell>();

            for (var i = 0; i < _shopData.SelectedSpells.Count; i++)
                selectedSpells.Add(spellsFactory.Create(_shopData.SelectedSpells[i]));

            Services.UiDataBinding.SetNewDataForGame(selectedSpells.ToArray());
        }
    }
}