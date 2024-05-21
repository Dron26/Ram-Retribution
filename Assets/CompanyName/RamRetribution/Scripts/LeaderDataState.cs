using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]
    public class LeaderDataState : ISaveable
    {
        public int Health;
        public int Damage;
        public int Armor;
        public float AttackSpeed;
        public List<int> Spells;
        
        public DataNames Name { get; } = DataNames.LeaderDataState;
    }
}