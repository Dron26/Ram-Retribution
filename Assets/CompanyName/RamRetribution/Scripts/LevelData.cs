namespace CompanyName.RamRetribution.Scripts
{
    public class LevelData
    {
        public int LevelNumber;    // Номер уровня
        public int MinLevelNumber; // Минимальный  уровень
        public int MaxLevelNumber; // Максимальный  уровень
        public int MaxBossHealth; // Максимальное здоровье босса на уровне
        public GateData GateData;   
        public SpawnData SpawnData;   
    
        public LevelData(SpawnData spawnData,GateData gateData,int levelNumber, int minLevelNumber, int maxLevelNumber,  int maxBossHealth)
        {
            SpawnData = spawnData;
            GateData = gateData;
            LevelNumber = levelNumber;
            MinLevelNumber = minLevelNumber;
            MaxLevelNumber = maxLevelNumber;
            MaxBossHealth = maxBossHealth;

        }
    }
}