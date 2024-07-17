using System;
using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;

namespace CompanyName.RamRetribution.Scripts.Lobby.GameShop
{
    public class Wallet
    {
        private readonly GameData _gameData;

        public Wallet(GameData gameData)
        {
            _gameData = gameData;
        }

        public event Action<CurrencyTypes,int> CurrencyChanged;
        
        public int Money => _gameData.Money;
        public int Horns => _gameData.Horns;

        public void Add(CurrencyTypes type ,int amount)
        {
            int result = 0;
            
            switch (type)
            {
                case CurrencyTypes.Money:
                    result = _gameData.Money + amount;
                    _gameData.Money = result;
                    break;
                case CurrencyTypes.Horns:
                    result = _gameData.Horns + amount;
                    _gameData.Horns = result;
                    break;
            }
            
            Services.PrefsDataService.Save(_gameData);
            CurrencyChanged?.Invoke(type,result);
        }

        public void Remove(CurrencyTypes type ,int amount)
        {
            int result = 0;
            
            switch (type)
            {
                case CurrencyTypes.Money:
                    result = _gameData.Money - amount;
                    _gameData.Money = result;
                    break;
                
                case CurrencyTypes.Horns:
                    result = _gameData.Horns - amount;
                    _gameData.Horns = result;
                    break;
            }
            
            Services.PrefsDataService.Save(_gameData);
            CurrencyChanged?.Invoke(type, result);
        }

        public bool IsEnough(CurrencyTypes type, int price)
        {
            return type switch
            {
                CurrencyTypes.Money => _gameData.Money >= price,
                CurrencyTypes.Horns => _gameData.Horns >= price,
                _ => throw new System.NotImplementedException($"Missing currency type {nameof(type)}")
            };
        }

        public void UpdateText()
        {
            CurrencyChanged?.Invoke(CurrencyTypes.Money, Money);
            CurrencyChanged?.Invoke(CurrencyTypes.Horns, Horns);
        }
    }
}