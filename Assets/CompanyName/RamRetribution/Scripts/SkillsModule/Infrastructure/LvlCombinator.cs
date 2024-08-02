using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Skills.Infrastructure
{
    public class LvlCombinator
    {
        private readonly GameDataBase _gameDataBase;
        private int _lvlNumber;

        public LvlCombinator(GameDataBase gameDataBase)
        {
            _gameDataBase = gameDataBase;
        }

        //Damage amplify compute
        public int GetCurrentLvlSpellDamage()
        {
            return Mathf.FloorToInt(_lvlNumber * _gameDataBase.DamageKooficient);
        }
    }
}
