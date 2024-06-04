using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common
{
    public class PauseControl
    {
        private bool _isFocusPause;
        private bool _isInGamePause;
        private bool _isUiPause;

        #region PauseAPI
        public void SetPauseOnFocus(bool value)
        {
            _isFocusPause = value;
            HandlePause();
        }

        public void SetInGamePause(bool value)
        {
            _isInGamePause = value;
            HandlePause();
        }

        public void SetPauseOnUI(bool value)
        {
            _isUiPause = value;
            HandlePause();
        }
        #endregion

        #region Logic
        private void HandlePause()
        {
            if (IsGameOnPause())
                Stop();
            else
                Continue();
        }

        private void Continue()
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }

        private void Stop()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }

        private bool IsGameOnPause()
        {
            return _isFocusPause || _isUiPause || _isInGamePause;
        }
        #endregion
    }
}