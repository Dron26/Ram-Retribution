using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.UI.HUD;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.UI.Shop
{
    [RequireComponent(typeof(Image))]
    public class ShopItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private PriceView _priceView;

        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _selectedImage;
        
        [SerializeField] private Sprite _defaultBackground;
        [SerializeField] private Sprite _highlightBackground;

        private Image _backgroundImage;

        public event Action<ShopItemView> Clicked;

        public ShopItem Item { get; private set; }
        public bool IsLock { get; private set; }
        public CurrencyTypes Currency => Item.CurrencyType;
        public int Price => Item.Cost;

        public void Init(ShopItem item)
        {
            _backgroundImage = GetComponent<Image>();
            _backgroundImage.sprite = _defaultBackground;

            Item = item;
            _nameField.text = item.Name;
            _contentImage.sprite = item.Sprite;
            _priceView.Show(item.CurrencyType, item.Cost);
        }

        public void OnPointerClick(PointerEventData eventData)
            => Clicked?.Invoke(this);

        public void Lock()
        {
            IsLock = true;
            _lockImage.gameObject.SetActive(true);
            _priceView.Show(Item.CurrencyType, Item.Cost);
        }

        public void Unlock()
        {
            IsLock = false;
            _lockImage.gameObject.SetActive(false);
            _priceView.Hide();
        }

        public void Select()
            => _selectedImage.gameObject.SetActive(true);

        public void Unselect()
            => _selectedImage.gameObject.SetActive(false);

        public void Highlight()
            => _backgroundImage.sprite = _highlightBackground;

        public void Unhighlight()
            => _backgroundImage.sprite = _defaultBackground;
    }
}