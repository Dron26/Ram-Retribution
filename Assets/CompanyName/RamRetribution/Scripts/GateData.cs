using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]
    public class GateData : ISave
    {
        public int Strength;
        public int HealthValue;

        public string Name { get; } = "GateData";
    }
}