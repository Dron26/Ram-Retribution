using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.UI.HUD;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.UI.Shop
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private PriceView _priceView;

        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;

        [Header("Tweeen config")]
        [SerializeField][Range(0.1f, 0.3f)] private float _shakeScaleDuration;
        [SerializeField] private float _xShakeScaleStrenght;
        [SerializeField] private float _yShakeScaleStrenght;

        [SerializeField][Range(0, 1)] private float _lockAnimationDuration;
        [SerializeField][Range(0.5f, 5)] private float _lockAnimationStrength;
        
        private bool _isLock;
        private Tween _scaleTweener;

        public event Action Clicked;

        private void OnEnable() => _button.onClick.AddListener(OnClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnClicked);

        public void UpdateText(CurrencyTypes currencyType ,int price) 
            => _priceView.Show(currencyType,price);

        public void Lock()
        {
            _isLock = true;
            _priceView.ChangeTextColor(_lockColor);

            _scaleTweener?.Kill();
        }

        public void Unlock()
        {
            _isLock = false;
            _priceView.ChangeTextColor(_unlockColor);

            _scaleTweener = transform.DOShakeScale(
                    duration: _shakeScaleDuration,
                    strength: new Vector3(_xShakeScaleStrenght, _yShakeScaleStrenght),
                    randomnessMode: ShakeRandomnessMode.Harmonic)
                .OnComplete(() => transform.localScale = Vector3.one);
        }

        private void OnClicked()
        {
            if (_isLock)
            {
                transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
                return;
            }

            Clicked?.Invoke();
        }
    }
}