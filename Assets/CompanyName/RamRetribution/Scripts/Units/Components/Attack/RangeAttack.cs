using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public class RangeAttack : IAttackComponent
    {
        private float _damage;
        private float _attackSpeed;
        private float _distance;

        public RangeAttack(float damage, float attackSpeed, float distance)
        {
            _damage = damage;
            _attackSpeed = attackSpeed;
            _distance = distance;
        }
        
        public AttackType AttackType => AttackType.Range;
        
        public void Attack(IDamageable damageable)
        {
            
        }
    }
}