using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay;
using UnityEngine;

public class ForTestGameEntryPoint : MonoBehaviour
{
    private LeaderDataState _leaderData;
    private ShopDataState _shopDataState;
    private GameData _gameData;
    private BattleBootstrap _battleBootstrap;

    private void Awake()
    {
        Services.Init();
        LoadLevel();
    }

    private void LoadLevel()
    {
        LoadData();
        InitBattle();
    }

    private void LoadData()
    {
        _leaderData = Services.PrefsDataService.Load<LeaderDataState>(
            DataNames.LeaderDataState.ToString());

        _shopDataState = Services.PrefsDataService.Load<ShopDataState>(
            DataNames.ShopDataState.ToString());

        _gameData = Services.PrefsDataService.Load<GameData>(
            DataNames.GameData.ToString());
    }

    private void InitBattle()
    {
        var battleCommander = Instantiate(Services
            .ResourceLoadService
            .Load<BattleCommander>($"{AssetPaths.CommonPrefabs}{nameof(BattleCommander)}"));

        var battleBootstrap = new BattleBootstrap(_gameData, _shopDataState.SelectedRams, _leaderData);
        battleBootstrap.Init(battleCommander);
    }
}
