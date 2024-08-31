using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class IncreaseAllyDamage : ISpell
    {
        private int _bonusValue;

        private const int FriendlyLayerMask = 8; // Add constants.cs for layers

        public IncreaseAllyDamage(LevelCombinator levelCombinator, Sprite image)
        {
            Image = image;
            _bonusValue *= levelCombinator.GetIncreaseDamageCoeficient();

        }
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log(" IncreseDamage SpellActivated");
            var results = new Collider[9];
        
            Physics.OverlapSphereNonAlloc(
                Services.LeaderTransform.position, 
                10, 
                results, 
                1 << FriendlyLayerMask);
        
            //Add particles and sound

            foreach (var enemy in results)
            {
                // if (enemy.TryGetComponent(out IRam ram))
                // {
                //     //ram.IncreseDamageValue; (Надо найти интерфейс или класс, через который можно на время увеличить урон Unit(Баранам)
                // }
            }
        }
    }
}
