using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Lobby;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.HUD;
using UnityEngine;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.Factories
{
    public class CanvasFactory
    {
        private readonly LobbyCanvas _lobbyCanvasPrefab;
        private readonly LobbyHUD _lobbyHUDPrefab;
        private readonly DiContainer _diContainer;

        public CanvasFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            var lobbyCanvas = _diContainer.Resolve<IResourceLoadService>()
                .Load<LobbyCanvas>($"{AssetPaths.ShopPrefabs}{nameof(LobbyCanvas)}");

            var lobbyHUD = _diContainer.Resolve<IResourceLoadService>()
                .Load<LobbyHUD>($"{AssetPaths.ShopPrefabs}{nameof(LobbyHUD)}");

            _lobbyCanvasPrefab = lobbyCanvas;
            _lobbyHUDPrefab = lobbyHUD;
        }

        public void CreateLobbyView()
        {
            ConstructLobbyCanvas();
            ConstructLobbyHUD();
        }
        
        private void ConstructLobbyCanvas()
        {
            var canvasInstance = Object.Instantiate(_lobbyCanvasPrefab);
            
            var shopContent = new ShopContent(_diContainer.Resolve<IResourceLoadService>());
            
            _diContainer.Inject(canvasInstance.Shop, new object[]
            {
                shopContent
            });
        }
        
        private void ConstructLobbyHUD() 
            => _diContainer.Inject( Object.Instantiate(_lobbyHUDPrefab));
    }
}