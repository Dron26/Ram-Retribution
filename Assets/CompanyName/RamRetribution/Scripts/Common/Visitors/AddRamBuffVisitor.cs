using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Rams;

namespace CompanyName.RamRetribution.Scripts.Common.Visitors
{
    public class AddRamBuffVisitor : IRamsVisitor
    {
        private readonly ConfigId _configId;

        public AddRamBuffVisitor(ConfigId configId)
        {
            _configId = configId;
        }
        
        public void Visit(Unit unit)
        {
            unit.Accept(this);
        }

        public void Visit(Leader leader)
        {
            throw new System.ArgumentException();
        }

        public void Visit(Tank tank)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Attacker attacker)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Demolisher demolisher)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(Support support)
        {
            throw new System.NotImplementedException();
        }
    }
}