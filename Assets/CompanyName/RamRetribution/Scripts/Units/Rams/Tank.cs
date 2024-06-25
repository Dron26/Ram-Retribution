using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Tank : Unit, IRam
    {
        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}