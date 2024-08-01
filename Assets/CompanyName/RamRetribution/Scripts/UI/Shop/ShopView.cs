using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Shop;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.UI.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;

        private List<ShopItemView> _itemViews;
        private ShopItemViewFactory _itemsFactory;

        private OpenItemChecker _openItemChecker;
        private SelectedItemChecker _selectedItemChecker;

        public Action<ShopItemView> ItemViewClicked;

        public void Init(OpenItemChecker openItemChecker, SelectedItemChecker selectedItemChecker)
        {
            _openItemChecker = openItemChecker;
            _selectedItemChecker = selectedItemChecker;
            _itemViews = new List<ShopItemView>();
            _itemsFactory = new ShopItemViewFactory();
        }

        public void CreateItemViews(IReadOnlyList<ShopItem> items)
        {
            Clear();

            foreach (var item in items)
            {
                var view = _itemsFactory.Create(item,_container);
                view.Clicked += OnShopItemClicked;
                
                view.Unselect();
                view.Unhighlight();
                
                _openItemChecker.Visit(view.Item);

                if (_openItemChecker.IsOpen)
                {
                    _selectedItemChecker.Visit(view.Item);

                    if (_selectedItemChecker.IsSelected)
                    {
                        if (_selectedItemChecker.IsSingleSelectItem)
                        {
                            view.Select();
                            view.Highlight();
                            ItemViewClicked?.Invoke(view);
                        }
                        
                        view.Select();
                    }
                    
                    view.Unlock();
                }
                else
                {
                    view.Lock();
                }

                _itemViews.Add(view);
            }
            
            Sort();
        }
        
        public void Select(ShopItemView view)
        {
            if (view.Item is RamItem)
            {
                foreach (var itemView in _itemViews)
                {
                    _selectedItemChecker.Visit(itemView.Item);
                    
                    if(_selectedItemChecker.IsSelected)
                        itemView.Select();
                    else
                        itemView.Unselect();
                }
                
                return;
            }
            
            foreach (var itemView in _itemViews)
                itemView.Unselect();

            view.Select();
        }

        private void OnShopItemClicked(ShopItemView view)
        {
            Highlight(view);
            ItemViewClicked?.Invoke(view);
        }

        private void Highlight(ShopItemView selectedView)
        {
            foreach (var view in _itemViews)
                view.Unhighlight();
            
            selectedView.Highlight();
        }
        
        private void Sort()
        {
            _itemViews = _itemViews
                .OrderBy(view => view.IsLock)
                .ThenBy(view => view.Price)
                .ToList();

            for (int i = 0; i < _itemViews.Count; i++)
                _itemViews[i].transform.SetSiblingIndex(i);
        }

        private void Clear()
        {
            for (int i = 0; i < _itemViews.Count; i++)
            {
                _itemViews[i].Clicked -= OnShopItemClicked;
                Destroy(_itemViews[i].gameObject);
            }

            _itemViews.Clear();
        }
    }
}