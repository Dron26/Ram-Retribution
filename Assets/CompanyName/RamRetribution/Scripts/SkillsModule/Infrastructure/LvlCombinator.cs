using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Infrastructure
{
    public class LvlCombinator
    {
        private GameDataBase _gameDataBase;
        private int _lvlNumber;

        public LvlCombinator(GameDataBase gameDataBase)
        {
            _gameDataBase = gameDataBase;
        }


        public int GetCurrentLvlSpellDamage()
        {
            return Mathf.FloorToInt(_lvlNumber * _gameDataBase.DamageKooficient);
        }
    }
}
