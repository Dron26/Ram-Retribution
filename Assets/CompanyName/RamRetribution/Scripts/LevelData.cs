using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]  
    public class LevelData : ISaveable
    {
        public int LevelNumber;
        public int BossHealth;

        public DataNames Name { get; } = DataNames.LevelData;
    }
}