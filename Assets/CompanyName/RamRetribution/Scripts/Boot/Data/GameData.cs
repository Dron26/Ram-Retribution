using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]  
    public class GameData : ISavable
    {
        public bool FirstEntry = true;
        public List<int> PassedLevels = new();
        public int Money = 100;
        public int Horns = 100;
        public int BrokenGates = 0;

        public DataNames Name => DataNames.GameData;

        public int GetLastPassedLevelIndex() 
            => PassedLevels.Count == 0 ? 1 : PassedLevels.LastOrDefault();
    }
}