using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class RageIncrease : ISpell
    {
        private readonly LevelCombinator _levelCombinator;
    
        public RageIncrease(LevelCombinator levelCombinator, Sprite sprite)
        {
            Image = sprite; 
            _levelCombinator = levelCombinator;
        }
        
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log("RageIncreaseSpell Activated");
            _levelCombinator.IncreaseRageValueAccumulation(); //Увеличивает на время увеличение накопления ярости
        }
    }
}
