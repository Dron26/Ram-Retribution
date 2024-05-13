using CompanyName.RamRetribution.Scripts.Ram;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IRamVisitor
    {
        public void Visit(Unit unit);
        public void Visit(Support support);
        public void Visit(Tank tank);
        public void Visit(Leader leader);
        public void Visit(Demolisher demolisher);
        public void Visit(Attacker attacker);
    }
}