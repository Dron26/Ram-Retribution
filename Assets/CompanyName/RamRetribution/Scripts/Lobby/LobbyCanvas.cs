using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Lobby
{
    public class LobbyCanvas : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        
        public Shop Shop => _shop;
    }
}