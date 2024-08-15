using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Tank : Unit, IRam, IPassiveSpellHolder
    {
        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Unit Instance { get; }
        
        public void ActivatePassiveSkill(List<Unit> units)
        {
            
        }

        public void DeactivatePassiveSkill(List<Unit> units)
        {
            
        }
    }
}