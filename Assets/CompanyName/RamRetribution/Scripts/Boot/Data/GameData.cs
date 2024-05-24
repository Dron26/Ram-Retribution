using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]  
    public class GameData : ISaveable
    {
        public bool FirstEntry;
        public int CurrentLevel;
        public int Gold;
        public int Horns;

        public DataNames Name => DataNames.LevelData;
    }
}