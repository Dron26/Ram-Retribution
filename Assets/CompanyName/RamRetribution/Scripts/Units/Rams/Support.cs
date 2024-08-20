using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Support : Unit, IRam, IBuffHolder
    {
        private float _percentOfHealing = 0.1f;

        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IRamsVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public void ActivateBuff(List<Unit> units, int lvlNumber)
        {
            
        }

        public void DeactivateBuff(List<Unit> units)
        {
            
        }
        
        public override void AddBuff(BuffData buffData)
        {
            throw new System.NotImplementedException();
        }
    }
}