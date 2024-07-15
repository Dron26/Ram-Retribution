using CompanyName.RamRetribution.Scripts.Boot;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        private GameLoseScreen _gameLoseScreen;
        private StateMachine _gameStateMachine;

        public void Init(StateMachine machine)
            => _gameStateMachine = machine;

        public void ShowLoseScreen()
        {
            var screenPrefab = Services
                .ResourceLoadService
                .Load<GameLoseScreen>($"{AssetPaths.CommonPrefabs}{nameof(GameLoseScreen)}");

            var loseScreen = Instantiate(screenPrefab);
            loseScreen.Init(_gameStateMachine);
        }
    }
}