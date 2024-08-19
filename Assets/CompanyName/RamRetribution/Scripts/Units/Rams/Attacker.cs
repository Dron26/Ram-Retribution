using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Attacker : Unit, IRam, IBuffHolder
    {
        private ImprovableUnitFields _improvableField;
        private float _improveBonus;
        
        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IRamsVisitor visitor) 
            => visitor.Visit(this);

        public void ActivateBuff(List<Unit> units, int lvlNumber)
        {
            _improveBonus *= lvlNumber;
            
            foreach (var unit in units)
            {
                if (unit.AttackComponent is IImprovable improvable)
                    improvable.Improve(ref GetImprovableField(unit),
                        _improveBonus);

                Debug.Log($"Unit: {unit.name} increase {_improvableField} on {_improveBonus}. " +
                          $"Current stats: Damage {unit.AttackComponent.Damage}, AS {unit.AttackComponent.AttackSpeed}");
            }
        }

        public void DeactivateBuff(List<Unit> units)
        {
            foreach (var unit in units)
            {
                if (unit.AttackComponent is IImprovable improvable)
                    improvable.UnImprove(ref GetImprovableField(unit),
                        _improveBonus);
            }
        }

        public override void AddBuff(BuffData buffData)
        {
            _improvableField = buffData.ImprovableField;
            _improveBonus = buffData.BonusValue;
        }

        private ref float GetImprovableField(Unit unit)
        {
            switch (_improvableField)
            {
                case ImprovableUnitFields.UnitDamage:
                    return ref unit.AttackComponent.Damage;
                case ImprovableUnitFields.AttackSpeed:
                    return ref unit.AttackComponent.AttackSpeed;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}