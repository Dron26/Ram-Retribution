using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using UnityEngine;
using Zenject;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]
    public class LeaderDataState : ISavable
    {
        public int HealthValue;
        public int ArmorValue;
        public int Damage;
        public float AttackSpeed;
        public ArmorTypes ArmorType;
        public AttackType AttackType;

        private UnitConfig _config;
        
        public LeaderDataState()
        {
            // var config = Services
            //     .ResourceLoadService
            //     .Load<ConfigsContainer>($"{AssetPaths.Configs}{nameof(ConfigsContainer)}")
            //     .Get(ConfigId.Leader);
            
            HealthValue = _config.HealthValue;
            ArmorValue = _config.ArmorValue;
            Damage = _config.Damage;
            AttackSpeed = _config.AttackSpeed;
            ArmorType = _config.ArmorType;
            AttackType = _config.AttackType;
            
            Debug.Log($"LeaderDataState inited");
        }
        
        public DataNames Name => DataNames.LeaderDataState;

        [Inject]
        private void Construct([Inject(Id = nameof(Leader))]UnitConfig config)
        {
            _config = config;
            Debug.Log($"{_config != null}");
        }
    }
}