using System;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.UI
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action Clicked;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClicked);
        }
        
        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}