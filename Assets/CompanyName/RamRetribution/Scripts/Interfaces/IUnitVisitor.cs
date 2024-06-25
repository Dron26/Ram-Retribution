using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using CompanyName.RamRetribution.Scripts.Units.Rams;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IUnitVisitor
    {
        public void Visit(Unit unit);
        public void Visit(Leader leader);
        public void Visit(Tank tank);
        public void Visit(Attacker attacker);
        public void Visit(Demolisher demolisher);
        public void Visit(Support support);
        public void Visit(LightEnemy lightEnemy);
    }
}