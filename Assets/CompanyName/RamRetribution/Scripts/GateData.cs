using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]
    public class GateData : ISaveable
    {
        public int Strength = 115;
        public int HealthValue = 115;

        public DataNames Name { get; } = DataNames.GateData;
    }
}