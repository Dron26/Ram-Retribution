using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.RamRetribution.Scripts.UI.Shop
{
    public class CategoryButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _unselectColor; 
        [SerializeField] private Sprite _selectSprite;
        [SerializeField] private Sprite _unselectSprite;
        [SerializeField] private float _selectOffsetY;
        [SerializeField] private float _unselectOffsetY;
        
        public event Action Clicked;
        
        private void OnEnable() =>
            _button.onClick.AddListener(OnClicked);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnClicked);
        
        public void Select()
        {
            _image.sprite = _selectSprite;
            _text.color = _selectColor;
            _text.rectTransform.anchoredPosition = new Vector2(0f,_selectOffsetY);
        }

        public void Unselect()
        {
            _image.sprite = _unselectSprite;
            _text.color = _unselectColor;
            _text.rectTransform.anchoredPosition = new Vector2(0f,_unselectOffsetY);
        }
        
        private void OnClicked() =>
            Clicked?.Invoke();
    }
}