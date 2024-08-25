using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Attack;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.SkillsVariant
{
    public class GateDemolish : ISpell
    {
        private readonly IAttackComponent _attackComponent;

        public GateDemolish(float baseDamage ,Sprite image, LvlCombinator lvlCombinator)
        {
            Image = image;
            _attackComponent = new MagicAttack(baseDamage, lvlCombinator);
        }
    
        public Sprite Image { get; }

        public void ActivateSkill()
        {
            Debug.Log(" GateDemolish spell activated");
            /*тут ошибка!!!!*/
            Transform gateTransform = Services.LeaderTransform; //Надо получить ворота со сцены, чтобы нанести урон, Где хранится ссылка на него?
            if (gateTransform.TryGetComponent(out IAttackble target))
                _attackComponent.Attack(target);
        }
    }
}
