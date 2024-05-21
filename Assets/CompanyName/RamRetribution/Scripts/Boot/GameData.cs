using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    [System.Serializable]
    public class GameData
    {
        public DataNames Name;
        public LeaderDataState LeaderDataState;
        public LevelData LevelData;
        public GateData GateData;
        public HiredRamsData HiredRamsData;
        
        public GameData(DataNames name, LeaderDataState leaderDataState, LevelData levelData, GateData gateData)
        {
            Name = name;
            LeaderDataState = leaderDataState;
            LevelData = levelData;
            GateData = gateData;
        }
    }
}