using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Infrastructure
{
    public class LvlCombinator
    {
        private GameDataBase _gameDataBase;
        public KooficientStructure KooficientStructureForHealth;
        public KooficientStructure KooficientStructureForDamage;



        private int _lvlNumber;
        private int _maxLvlNumber = 520;

        public LvlCombinator(GameDataBase gameDataBase)
        {
            _gameDataBase = gameDataBase;
        }

        public float GetCurrenntLvlHealth(int baseHealth)
        {
            return baseHealth + (baseHealth * KooficientStructureForHealth.IndexI.Evaluate(_lvlNumber / _maxLvlNumber)
                * (baseHealth * KooficientStructureForHealth.IndexK.Evaluate(_lvlNumber / _maxLvlNumber)));
        }
        public float GetCurrenntLvlDamage(int baseDamage)
        {
            return baseDamage + (baseDamage * KooficientStructureForDamage.IndexI.Evaluate(_lvlNumber / _maxLvlNumber)
                * (baseDamage * KooficientStructureForDamage.IndexK.Evaluate(_lvlNumber / _maxLvlNumber)));
        }

        public int GetCurrentLvlSpellDamage()
        {
            return Mathf.FloorToInt(_lvlNumber * _gameDataBase.DamageKooficient);
        }
    }
}
