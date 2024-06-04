using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }

        public abstract void Accept(IShopItemVisitor visitor);
    }
}