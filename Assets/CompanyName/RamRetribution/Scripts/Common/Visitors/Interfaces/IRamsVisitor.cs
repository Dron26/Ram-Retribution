using CompanyName.RamRetribution.Scripts.Units.Rams;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors.Interfaces
{
    public interface IRamsVisitor
    {
        public void Visit(Ram unit);
        public void Visit(Leader leader);
        public void Visit(Tank tank);
        public void Visit(Attacker attacker);
        public void Visit(Demolisher demolisher);
        public void Visit(Support support);
    }
}