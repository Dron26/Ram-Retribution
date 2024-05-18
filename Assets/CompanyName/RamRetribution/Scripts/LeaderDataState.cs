using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts
{
    [System.Serializable]
    public class LeaderDataState : ISave
    {
        public int Health;
        public int Damage;
        public int Armor;
        public float AttackSpeed;
        public List<int> Spells; 

        public string Name { get; } = "LeaderData";
    }
}