using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Enemies
{
    public class LightEnemy : Unit, IEnemy
    {
        public override UnitTypes Type => UnitTypes.Enemy;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}