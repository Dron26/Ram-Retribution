using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates
{
    public class LobbyBootstrapState : IState
    {
        private readonly ISaveLoadDataService _saveLoadDataService;
        
        private readonly GameData _gameData;
        private readonly ShopDataState _shopData;

        public LobbyBootstrapState(ISaveLoadDataService saveLoadDataService, GameData gameData, ShopDataState shopData) 
        {
            _saveLoadDataService = saveLoadDataService;
            _gameData = gameData;
            _shopData = shopData;
        }

        public void Enter()
        {
            SceneManager.LoadSceneAsync(SceneNames.Lobby);
        }

        public void Exit()
        {
            _saveLoadDataService.Save(_gameData);
            _saveLoadDataService.Save(_shopData);
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