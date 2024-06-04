using CompanyName.RamRetribution.Scripts.Common.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.UI.HUD
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private Image _currencyImage;
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Sprite _coin;
        [SerializeField] private Sprite _horn;
        
        public void Show(CurrencyTypes currencyType, int price)
        {
            switch (currencyType)
            {
                case CurrencyTypes.Money:
                    _currencyImage.sprite = _coin;
                    break;
                case CurrencyTypes.Horns:
                    _currencyImage.sprite = _horn;
                    break;
            }
            
            _text.gameObject.SetActive(true);
            _currencyImage.gameObject.SetActive(true);
            _text.text = price.ToString();
        }

        public void ChangeTextColor(Color newColor)
        {
            _text.color = newColor;
        }

        public void Hide()
        {
             _text.gameObject.SetActive(false);
            _currencyImage.gameObject.SetActive(false);
        }
    }
}