using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]  
    public class GameData : ISavable
    {
        public bool FirstEntry;
        public List<int> PassedLevels;
        public int Money;
        public int Horns;
        public int BrokenGates;

        public GameData()
        {
            FirstEntry = true;
            PassedLevels = new List<int>();
            Money = 100;
            Horns = 100;
            BrokenGates = 0;
        }
        
        public DataNames Name => DataNames.GameData;

        public int GetLastPassedLevelIndex() 
            => PassedLevels.Count == 0 ? 1 : PassedLevels.LastOrDefault();
    }
}