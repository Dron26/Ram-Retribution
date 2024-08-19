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

        public float GetUnitHealth(int baseHealth)
        {
            return baseHealth + (baseHealth * _gameDataBase.UnitHealthCurve.IndexI.Evaluate(_levelNumber / _maxLevelNumber)
                * (baseHealth * _gameDataBase.UnitHealthCurve.IndexK.Evaluate(_levelNumber / _maxLevelNumber)));
        }
        
        public float GetUnitDamage(int baseDamage)
        {
            return baseDamage + (baseDamage * _gameDataBase.UnitDamageCurve.IndexI.Evaluate(_levelNumber / _maxLevelNumber)
                * (baseDamage * _gameDataBase.UnitDamageCurve.IndexK.Evaluate(_levelNumber / _maxLevelNumber)));
        }
        
        public int GetSpellDamage()
        {
            return Mathf.FloorToInt(_levelNumber * _gameDataBase.DamageBonusPerLevel);
        }

        public int GetHealingSpellValue()
        {
            return Mathf.FloorToInt(_levelNumber * _gameDataBase.HealthBonusPerLevel);
        }

        public void AddGold()
        {
            _gameDataBase.Gold += _gameDataBase.GoldSpellValue;
        }

        public int GetIncreaseDamageCoeficient()
        {
            return 0;
        }

        public int GetSpawnRamsCountValue()
        {
            return 0;
        }

        public void IncreaseRageValueAccumulation()
        {
            _gameDataBase.RageAccumulationBonus *= 2;
            //UnitTask.Delay(TimeSpam.FromSeconds(10));
            _gameDataBase.RageAccumulationBonus /= 2;
        }

        private void OnLevelStarting(int number) 
            => _levelNumber = number;
    }
}
