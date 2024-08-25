using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Factorys.Interfaces
{
    public interface IGateFactory
    {
        public Gate Create(Gate instance);
    }
}