using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]  
    public class GameData : ISaveable
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

        public Level TryLoadLevel(int number)
        {
            if (PassedLevels.Count == 0)
                return new Level(1);
            
            return PassedLevels.Contains(number) ? new Level(number) : null;
        }
    }
}