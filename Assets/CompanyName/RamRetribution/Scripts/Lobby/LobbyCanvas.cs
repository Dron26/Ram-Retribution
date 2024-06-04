using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Lobby
{
    public class LobbyCanvas : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        
        public Shop Shop => _shop;
    }
}