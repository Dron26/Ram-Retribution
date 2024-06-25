using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class MeleeAttack : IAttackComponent
    {
        private const float AttackDistance = 5f;
        private readonly int _damage;

        public MeleeAttack(int damage, float attackSpeed)
        {
            _damage = damage;
            AttackSpeed = attackSpeed;
        }

        public float AttackSpeed { get; }
        public int Damage => _damage;
        public float Distance => AttackDistance;
        public AttackType AttackType => AttackType.Melee;
        
        public void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(AttackType, _damage);
        }
    }
}