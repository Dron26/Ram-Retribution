using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]
    public class LeaderDataState : ISaveable
    {
        public UnitConfig Config;
        public List<int> Spells;

        public LeaderDataState()
        {
            Config = Services.ResourceLoadService.Load<UnitConfig>($"{AssetPaths.Configs}{nameof(Leader)}");
            Spells = new List<int>();
        }
        
        public DataNames Name => DataNames.LeaderDataState;
    }
}