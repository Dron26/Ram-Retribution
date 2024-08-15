using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using CompanyName.RamRetribution.Scripts.Units.Components;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Attacker : Unit, IRam, IPassiveSpellHolder
    {
        public override UnitTypes Type => UnitTypes.Ram;
        public Unit Instance => this;
        
        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void ActivatePassiveSkill(List<Unit> units)
        {
            foreach (var unit in units)
            {
                if(unit.AttackComponent is IImprovable improvable)
                    improvable.Improve(
                        Services.GameDataBase.DamageBonus, 
                        ref unit.AttackComponent.Damage); 
            }
        }

        public void DeactivatePassiveSkill(List<Unit> units)
        {
            foreach (var unit in units)
            {
                if(unit.AttackComponent is IImprovable improvable)
                    improvable.UnImprove(
                        Services.GameDataBase.DamageBonus, 
                        ref unit.AttackComponent.Damage);
            }
        }
    }
}