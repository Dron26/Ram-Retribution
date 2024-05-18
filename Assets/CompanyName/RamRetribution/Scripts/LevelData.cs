using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]  
    public class LevelData : ISave
    {
        public int LevelNumber;
        public int BossHealth;

        public string Name { get; } = "LevelData";
    }
}