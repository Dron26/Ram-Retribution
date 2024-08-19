using System.Collections;
using Agava.YandexGames;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.GameStates;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            yield return YandexGamesSdk.Initialize(OnInitialized);
#else
            OnInitialized();
#endif

            StateMachine gameStateMachine = new StateMachine();
            gameStateMachine.SetState<LobbyBootstrapState>();
            
            yield break;
        }

        private void OnInitialized()
        {
            Services.InitProjectCtx();
        }
    }
}