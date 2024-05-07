using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;

namespace _Project.CodeBase.Scripts
{
    public sealed class BootstrapEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            yield return YandexGamesSdk.Initialize(OnInit);
#else
            OnInit();
            
            yield break;
#endif
        }

        private void OnInit()
        {
            PlayerPrefs.Load(OnLoadCloudDataSuccessCallback);
        }

        private void OnLoadCloudDataSuccessCallback()
        {
            SceneManager.LoadScene(SceneNames.Lobby);
        }
    }
}