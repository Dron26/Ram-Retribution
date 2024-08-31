using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Leader : Ram
    {
        public override void Accept(IRamsVisitor visitor) 
            => visitor.Visit(this);

        public override void AddBuff(BuffData buffData)
        {
            throw new System.NotImplementedException();
        }
    }
}