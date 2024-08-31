using System;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Lobby.GameShop;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class LevelCombinator : IDisposable
    {
        private readonly GameDataBase _gameDataBase;
        private readonly Wallet _wallet;
        private readonly float _maxLevelNumber;

        private Game _game;
        private float _levelNumber;

        public LevelCombinator(GameDataBase gameDataBase, Wallet wallet)
        {
            _gameDataBase = gameDataBase;
            _wallet = wallet;
            _maxLevelNumber = GameConstants.MaxLevels;
        }

        public void Dispose()
        {
            _game.LevelStarting -= OnLevelStarting;
            _game.EnemyDefeated -= OnEnemyDefeated;
            _game.GatesDestroyed -= OnGatesDestroyed;
        }

        public void SubscribeOnGameEvents(Game game)
        {
            _game = game;
            
            _game.LevelStarting += OnLevelStarting;
            _game.EnemyDefeated += OnEnemyDefeated;
            _game.GatesDestroyed += OnGatesDestroyed;
        }
        
        private void OnLevelStarting(int number) 
            => _levelNumber = number;

        private void OnEnemyDefeated(Enemy enemy) 
            => _wallet.Add(enemy.RewardCurrency, GetGoldPerUnit(enemy));

        private void OnGatesDestroyed(GateTypes gateType) 
            => _wallet.Add(CurrencyTypes.Money, GetGoldPerGate(gateType));

        #region UnitsCurves

        public float GetUnitHealth(int baseHealth)
            => baseHealth +
               (baseHealth * _gameDataBase.UnitHealthCurve.IndexI.Evaluate(_levelNumber / _maxLevelNumber)
                           * (baseHealth *
                              _gameDataBase.UnitHealthCurve.IndexK.Evaluate(_levelNumber / _maxLevelNumber)));

        public float GetUnitDamage(int baseDamage)
            => baseDamage +
               (baseDamage * _gameDataBase.UnitDamageCurve.IndexI.Evaluate(_levelNumber / _maxLevelNumber)
                           * (baseDamage *
                              _gameDataBase.UnitDamageCurve.IndexK.Evaluate(_levelNumber / _maxLevelNumber)));

        private int GetGoldPerUnit(Enemy enemy)
        {
            var baseValue = enemy switch
            {
                LightEnemy  => _gameDataBase.BaseGoldPerLightEnemy,
                MediumEnemy => _gameDataBase.BaseGoldPerMediumEnemy,
                HeavyEnemy => _gameDataBase.BaseGoldPerHeavyEnemy,
                _ => throw new ArgumentOutOfRangeException($"Unknown enemy type: {enemy.GetType()}")
            };
            
            return Mathf.FloorToInt(baseValue +
                                    baseValue * _gameDataBase.GoldPerEnemyCurve.IndexI.Evaluate(_levelNumber /
                                                  _maxLevelNumber)
                                              * (baseValue *
                                                 _gameDataBase.GoldPerEnemyCurve.IndexK.Evaluate(_levelNumber /
                                                     _maxLevelNumber)));
        }

        #endregion

        #region GateCurves

        public int GetGateHealth(GateTypes type)
        {
            var baseHealth = _gameDataBase.GetGateHealth(type);
            
            return Mathf.FloorToInt( baseHealth +
                   (baseHealth * _gameDataBase.UnitHealthCurve.IndexI.Evaluate(_levelNumber / _maxLevelNumber)
                               * (baseHealth *
                                  _gameDataBase.UnitHealthCurve.IndexK.Evaluate(_levelNumber / _maxLevelNumber))));
        }

        public int GetGateArmor(GateTypes type)
        {
            var baseArmor = _gameDataBase.GetGateArmor(type);
            
            return Mathf.FloorToInt( baseArmor +
                   (baseArmor * _gameDataBase.UnitHealthCurve.IndexI.Evaluate(_levelNumber / _maxLevelNumber)
                              * (baseArmor *
                                 _gameDataBase.UnitHealthCurve.IndexK.Evaluate(_levelNumber / _maxLevelNumber))));
        }

        #endregion
        
        #region SpellsValues

        private int GetGoldPerGate(GateTypes gateTypes)
        {
            var baseGoldValue = gateTypes switch
            {
                GateTypes.Wood => _gameDataBase.BaseGoldPerWoodGate,
                GateTypes.Rock => _gameDataBase.BaseGoldPerRockGate,
                _ => throw new ArgumentOutOfRangeException(nameof(gateTypes), gateTypes, null)
            };
            
            return Mathf.FloorToInt(_levelNumber * baseGoldValue);
        }

        public int GetSpellDamage()
            => Mathf.FloorToInt(_levelNumber * _gameDataBase.DamageBonusPerLevel);

        public int GetHealingSpellValue()
            => Mathf.FloorToInt(_levelNumber * _gameDataBase.HealthBonusPerLevel);

        public void AddGold()
        {
            //_gameDataBase.Gold += _gameDataBase.GoldSpellValue;
        }

        public int GetIncreaseDamageCoeficient()
            => 0;

        public int GetSpawnRamsCountValue()
            => 0;

        public void IncreaseRageValueAccumulation()
        {
            _gameDataBase.RageAccumulationBonus *= 2;
            //UnitTask.Delay(TimeSpam.FromSeconds(10));
            _gameDataBase.RageAccumulationBonus /= 2;
        }

        #endregion
    }
}