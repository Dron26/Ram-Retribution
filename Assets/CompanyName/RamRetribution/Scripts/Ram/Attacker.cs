using CompanyName.RamRetribution.Scripts.Boot.SO;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class Attacker : Unit
    {
        private HealthComponent _health;

        public void Init(UnitDTO dto, int totalLeadStats)
        {
            
        }
        
        public override void TakeDamage(int amount)
        {
            _health.TakeDamage(amount);
        }

        public override void Heal(int amount)
        {
            
        }

        public override void Move(Vector3 target)
        {
            
        }
    }
}