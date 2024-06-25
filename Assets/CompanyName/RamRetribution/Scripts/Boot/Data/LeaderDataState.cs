using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.Data
{
    [System.Serializable]
    public class LeaderDataState : ISaveable
    {
        public int HealthValue;
        public int ArmorValue;
        public int Damage;
        public float AttackSpeed;
        public ArmorTypes ArmorType;
        public AttackType AttackType;
        public List<int> Spells;

        public LeaderDataState()
        {
            var config = Services
                .ResourceLoadService
                .Load<ConfigsContainer>($"{AssetPaths.Configs}{nameof(ConfigsContainer)}")
                .GetConfig($"Leader");
            
            HealthValue = config.HealthValue;
            ArmorValue = config.ArmorValue;
            Damage = config.Damage;
            AttackSpeed = config.AttackSpeed;
            ArmorType = ArmorTypes.Medium;
            AttackType = AttackType.Melee;
            
            Spells = new List<int>();
        }
        
        public DataNames Name => DataNames.LeaderDataState;
    }
}