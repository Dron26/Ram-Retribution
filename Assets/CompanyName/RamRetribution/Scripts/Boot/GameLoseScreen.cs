using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class GameLoseScreen : MonoBehaviour
    {
        [SerializeField] private Image _panel;
        [SerializeField] private Button _returnButton;

        private Canvas _canvas;
        private StateMachine _stateMachine;

        private void OnEnable() 
            => _returnButton.onClick.AddListener(OnReturnClicked);

        private void OnDisable() 
            => _returnButton.onClick.RemoveListener(OnReturnClicked);

        public void Init(StateMachine stateMachine) 
            => _stateMachine = stateMachine;

        private void OnReturnClicked() 
            => _stateMachine.SetState<LobbyBootstrapState>();
    }
}