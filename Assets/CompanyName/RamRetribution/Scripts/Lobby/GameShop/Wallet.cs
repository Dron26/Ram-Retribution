using System;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    public class Wallet
    {
        private readonly GameData _gameData;

        public Wallet(GameData gameData)
            => _gameData = gameData;

        public event Action<CurrencyTypes, int> CurrencyChanged;

        private int Money => _gameData.Money;
        private int Horns => _gameData.Horns;

        public void Add(CurrencyTypes type, int amount)
        {
            var result = GetCurrencyValue(type) + amount;
            
            SetCurrencyValue(type, result);
            CurrencyChanged?.Invoke(type, result);
        }

        public void Remove(CurrencyTypes type, int amount)
        {
            var result = GetCurrencyValue(type) - amount;

            SetCurrencyValue(type, result);
            CurrencyChanged?.Invoke(type, result);
        }

        public bool IsEnough(CurrencyTypes type, int price)
        {
            return type switch
            {
                CurrencyTypes.Money => _gameData.Money >= price,
                CurrencyTypes.Horns => _gameData.Horns >= price,
                _ => throw new ArgumentException($"Missing currency type {nameof(type)}")
            };
        }

        public void UpdateText()
        {
            CurrencyChanged?.Invoke(CurrencyTypes.Money, Money);
            CurrencyChanged?.Invoke(CurrencyTypes.Horns, Horns);
        }

        private int GetCurrencyValue(CurrencyTypes type)
        {
            return type switch
            {
                CurrencyTypes.Money => _gameData.Money,
                CurrencyTypes.Horns => _gameData.Horns,
                _ => throw new ArgumentException(nameof(type), $"Missing currency type {nameof(type)}")
            };
        }

        private void SetCurrencyValue(CurrencyTypes type, int value)
        {
            switch (type)
            {
                case CurrencyTypes.Money:
                    _gameData.Money = value;
                    break;
                case CurrencyTypes.Horns:
                    _gameData.Horns = value;
                    break;
                default:
                    throw new ArgumentException(nameof(type), $"Missing currency type {nameof(type)}");
            }
        }
    }
}