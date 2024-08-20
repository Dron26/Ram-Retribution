using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class RageIncrease : ISpell
    {
        private readonly LvlCombinator _lvlCombinator;
    
        public RageIncrease(LvlCombinator lvlCombinator, Sprite sprite)
        {
            Image = sprite; 
            _lvlCombinator = lvlCombinator;
        }
        
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log("RageIncreaseSpell Activated");
            _lvlCombinator.IncreaseRageValueAccumulation(); //Увеличивает на время увеличение накопления ярости
        }
    }
}
