using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    public class MeleeAttack : IAttackComponent
    {
        private int _damage;
        private float _attackSpeed;
        
        public MeleeAttack(int damage, float attackSpeed)
        {
            _damage = damage;
            _attackSpeed = attackSpeed;
        }
        
        public AttackType AttackType => AttackType.Melee;
        
        public void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(AttackType, _damage);
        }
    }
}