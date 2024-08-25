using System.Collections;
using Agava.YandexGames;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using UnityEngine;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        private StateMachine _gameStateMachine;
        
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            yield return YandexGamesSdk.Initialize();
#else
            //OnInitialized();
#endif
            
            _gameStateMachine.SetState<LobbyBootstrapState>();
            
            yield break;
        }

        [Inject]
        private void Construct(StateMachine machine) 
            => _gameStateMachine = machine;
    }
}