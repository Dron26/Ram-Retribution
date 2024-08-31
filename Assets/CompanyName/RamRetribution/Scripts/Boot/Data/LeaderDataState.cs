using CompanyName.RamRetribution.Scripts.Boot.Data.Interfaces;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Rams;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]
    public class LeaderDataState : ISavable, IInitializableData
    {
        public int HealthValue;
        public int ArmorValue;
        public int Damage;
        public float AttackSpeed;
        public ArmorTypes ArmorType;
        public AttackType AttackType;
        
        public DataNames Name => DataNames.LeaderDataState;
        
        public void Init(IResourceLoadService loadService)
        {
            var config = loadService
                .Load<ConfigsContainer>($"{AssetPaths.Configs}{nameof(ConfigsContainer)}")
                .Get(ConfigId.Leader);

            HealthValue = config.HealthValue;
            ArmorValue = config.ArmorValue;
            Damage = config.Damage;
            AttackSpeed = config.AttackSpeed;
            ArmorType = config.ArmorType;
            AttackType = config.AttackType;
        }
    }
}