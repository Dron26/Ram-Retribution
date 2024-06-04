using Agava.YandexGames;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.YandexSDK.Leaderboard
{
    public class LeaderboardButton : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _view;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _authorizedWindow;
        [SerializeField] private Button _authorizedButton;
        [SerializeField] private Button _authorizedCloseButton;

        private Button _open;
        private Leaderboard _leaderboard;

        private void Awake()
        {
            _open = GetComponent<Button>();
            _leaderboard = GetComponent<Leaderboard>();
        }

        private void OnEnable()
        {
            _open.onClick.AddListener(OnOpenClicked);
            _authorizedButton.onClick.AddListener(OnAuthorizedClicked);
            _closeButton.onClick.AddListener(HideLeaderBoard);
            _authorizedCloseButton.onClick.AddListener(HideAuthorizedWindow);
        }

        private void OnDisable()
        {
            _open.onClick.RemoveListener(OnOpenClicked);
            _authorizedButton.onClick.RemoveListener(OnAuthorizedClicked);
            _closeButton.onClick.RemoveListener(HideLeaderBoard);
            _authorizedCloseButton.onClick.RemoveListener(HideAuthorizedWindow);
        }

        private void OnOpenClicked()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        if (PlayerAccount.IsAuthorized == false)
        {
            Services.PauseControl.SetPauseOnUI(true);            
            _authorizedWindow.gameObject.SetActive(true);
            return;
        }
#endif
            if (_view.isActiveAndEnabled)
                HideLeaderBoard();
            else
                OpenLeaderboard();
        }

        private void OpenLeaderboard()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        if (PlayerAccount.IsAuthorized == true)
            PlayerAccount.RequestPersonalProfileDataPermission();
#endif
            _view.gameObject.SetActive(true);
            Services.PauseControl.SetPauseOnUI(true);
            
            var data = Services.PrefsDataService.Load<GameData>(
                DataNames.GameData.ToString());
            
            _leaderboard.SetPlayer(data.BrokenGates);
            _leaderboard.Fill();
        }

        private void OnAuthorizedClicked()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        PlayerAccount.Authorize(onSuccessCallback: () =>
        {
            HideAuthorizedWindow();
            OpenLeaderboard();
        });
#endif
        }

        private void HideLeaderBoard()
        {
            Services.PauseControl.SetPauseOnUI(false);
            _view.gameObject.SetActive(false);
        }

        private void HideAuthorizedWindow()
        {
            Services.PauseControl.SetPauseOnUI(false);
            _authorizedWindow.gameObject.SetActive(false);
        }
    }
}