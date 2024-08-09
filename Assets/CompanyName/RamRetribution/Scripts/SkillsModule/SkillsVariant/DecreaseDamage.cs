using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Infrastructure;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class DecreaseDamage : ISpell
    {
        private readonly LvlCombinator _lvlCombinator;
    
        public DecreaseDamage(LvlCombinator lvlCombinator, Sprite spellImage)
        {
            _lvlCombinator = lvlCombinator;
            Image = spellImage;
        }

        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log("Deacrese damage SpellActivated");
            var leaderTransform = Services.LeaderTransform;
            var results = new Collider[9];
            Physics.OverlapSphereNonAlloc(leaderTransform.position, 10, results, 1 << GameConstants.EnemyLayerMask);
            //Add particles and sound

            foreach (var enemy in results)
            {
                if (enemy.TryGetComponent(out IAttackble attackble)) // IAttackble ��������?
                {
                    //���� ����� ����� ��� ��������� ����� ������� ����� �� ����� ��������� ����� � ������
                }
            }
        }
    }
}
