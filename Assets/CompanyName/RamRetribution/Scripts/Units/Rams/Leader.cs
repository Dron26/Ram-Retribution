using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Leader : Ram
    {
        public override void Accept(IRamsVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public override void AddBuff(BuffData buffData)
        {
            throw new System.NotImplementedException();
        }
    }
}