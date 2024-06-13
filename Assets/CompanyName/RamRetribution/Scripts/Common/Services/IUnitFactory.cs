using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Common.Services
{
    public interface IUnitFactory
    {
        public Unit Create(UnitConfig config);
    }
}