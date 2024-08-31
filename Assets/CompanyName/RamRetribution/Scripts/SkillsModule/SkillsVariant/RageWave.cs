using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Attack;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class RageWave : ISpell
    {
        private readonly IAttackComponent _attackComponent;

        public RageWave(float baseDamage, Sprite sprite, LevelCombinator combinator)
        {
            Image = sprite;
            _attackComponent = new MagicAttack(baseDamage, combinator);
        }

        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log("SpellActivated");
            var results = new Collider[9];
            
            Physics.OverlapSphereNonAlloc(
                Services.LeaderTransform.position, 
                10, 
                results, 
                1 << GameConstants.EnemyLayerMask);
            
            //Add particles and sound
        
            foreach (var enemy in results)
            {
                if (enemy.TryGetComponent(out IAttackble target))
                    _attackComponent.Attack(target);
            }
        }
    }
}