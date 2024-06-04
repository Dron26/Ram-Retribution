using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]  
    public class GameData : ISaveable
    {
        public bool FirstEntry;
        public int CurrentLevel;
        public int Money;
        public int Horns;
        public int BrokenGates;

        public GameData()
        {
            FirstEntry = true;
            CurrentLevel = 1;
            Money = 100;
            Horns = 100;
            BrokenGates = 0;
        }
        
        public DataNames Name => DataNames.GameData;
    }
}