using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Attacker : Ram, IBuffHolder
    {
        private ImprovableUnitFields _improvableField;
        private float _improveBonus;

        public override void Accept(IRamsVisitor visitor) 
            => visitor.Visit(this);

        public void ActivateBuff(List<Unit> units, int levelNumber)
        {
            var bonus = _improveBonus * levelNumber;
            
            foreach (var unit in units)
            {
                if (unit.AttackComponent is IImprovable improvable)
                    improvable.Improve(ref GetImprovableField(unit),
                        bonus);

                Debug.Log($"Unit: {unit.name} increased {_improvableField} on {bonus}. " +
                          $"Current stats: Damage {unit.AttackComponent.Damage}, AS {unit.AttackComponent.AttackSpeed}");
            }
        }

        public void DeactivateBuff(List<Unit> units, int levelNumber)
        {
            var bonus = _improveBonus * levelNumber;
            
            foreach (var unit in units)
            {
                if (unit.AttackComponent is IImprovable improvable)
                    improvable.UnImprove(ref GetImprovableField(unit),
                        bonus);
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