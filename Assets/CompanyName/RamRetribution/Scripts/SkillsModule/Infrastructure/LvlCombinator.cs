using System;
using CompanyName.RamRetribution.Scripts.Skills.Infrastructure;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.Infrastructure
{
    public class LvlCombinator
    {
        private readonly GameDataBase _gameDataBase;
        private int _lvlNumber;

        public LvlCombinator(GameDataBase gameDataBase)
        {
            _gameDataBase = gameDataBase;
        }
        
        public int GetCurrentLvlSpellDamage()
        {
            return Mathf.FloorToInt(_lvlNumber * _gameDataBase.DamageKooficient);
        }

        public int GetHealingSpellValue()
        {
            return Mathf.FloorToInt(_lvlNumber * _gameDataBase.HealingKooficient);
        }

        internal void AddGoldFromSpell()
        {
            _gameDataBase.Gold += _gameDataBase.GoldSpellValue;
        }

        internal int GetIncreaseDamageCoeficient()
        {
            throw new NotImplementedException();
        }

        internal int GetSpawnRamsCountValue()
        {
            throw new NotImplementedException();
        }

        internal void IncreaseRageValueAccumulation()
        {
            _gameDataBase.RageAccumulationKooficient *= 2;
            //UnitTask.Delay(TimeSpam.FromSeconds(10));
            _gameDataBase.RageAccumulationKooficient /= 2;
        }
    }
}
