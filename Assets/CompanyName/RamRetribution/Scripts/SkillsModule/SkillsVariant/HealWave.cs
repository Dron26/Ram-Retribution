using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class HealWave : ISpell
    {
        private LevelCombinator _levelCombinator;
        private const int FriendlyLayerMask = 8; // Add constants.cs for layers
        private int _baseHealingValue;

        public HealWave(LevelCombinator levelCombinator, Sprite skillImage)
        {
            _levelCombinator = levelCombinator;
            Image = skillImage;
            _baseHealingValue *= _levelCombinator.GetHealingSpellValue();
        }
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log(" Heal SpellActivated");
            var leaderTransform = Services.LeaderTransform;
            var results = new Collider[9];
            Physics.OverlapSphereNonAlloc(leaderTransform.position, 10, results, 1 << FriendlyLayerMask);
            //Add particles and sound

            foreach (var enemy in results)
            {
                // if (enemy.TryGetComponent(out IRam ram))
                // {
                //     //ram.Heal(_baseHealingValue);
                // }
            }
        }
    }
}
