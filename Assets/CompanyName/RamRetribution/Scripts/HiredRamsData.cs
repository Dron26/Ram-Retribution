using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]
    public class HiredRamsData : ISaveable
    {
        public DataNames Name { get; } = DataNames.HiredRamsData;
    }
}