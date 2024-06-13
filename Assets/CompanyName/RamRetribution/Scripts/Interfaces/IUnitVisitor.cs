using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IUnitVisitor
    {
        public void Visit(Unit unit);
        public void Visit(IRam ram);
        public void Visit(IEnemy enemy);
        public void Visit(Squad squad);
    }
}