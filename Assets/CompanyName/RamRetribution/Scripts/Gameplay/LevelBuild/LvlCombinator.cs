using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild
{
    public class LvlCombinator
    {
        private readonly GameDataBase _gameDataBase;
        private readonly float _maxLevelNumber;

        private float _levelNumber;

        public LvlCombinator(GameDataBase gameDataBase)
        {
            _gameDataBase = gameDataBase;
            _maxLevelNumber = GameConstants.MaxLevels;
        }

        public void SubscribeToGameEvents(Game game)
            => game.LevelStarting += OnLevelStarting;

        public void UnSubscribeFromGameEvents(Game game)
            => game.LevelStarting -= OnLevelStarting;

        #region Curves

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

        public int GetGoldForUnit()
        {
            var baseValue = _gameDataBase.BaseGoldPerUnit;

            Debug.Log(baseValue +
                      baseValue * _gameDataBase.GoldPerUnitCurve.IndexI.Evaluate(_levelNumber /
                                    _maxLevelNumber)
                                * (baseValue *
                                   _gameDataBase.GoldPerUnitCurve.IndexK.Evaluate(_levelNumber /
                                       _maxLevelNumber)));
            
            return Mathf.FloorToInt(baseValue +
                                    baseValue * _gameDataBase.GoldPerUnitCurve.IndexI.Evaluate(_levelNumber /
                                                  _maxLevelNumber)
                                              * (baseValue *
                                                 _gameDataBase.GoldPerUnitCurve.IndexK.Evaluate(_levelNumber /
                                                     _maxLevelNumber)));
        }

        #endregion

        #region SpellsValues

        public int GetGoldForGate()
            => Mathf.FloorToInt(_levelNumber * _gameDataBase.BaseGoldPerGate);

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

        private void OnLevelStarting(int number)
            => _levelNumber = number;
    }
}