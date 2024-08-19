using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class RangeAttack : IAttackComponent
    {
        private readonly float _distance;
        private float _damage;
        private float _attackSpeed;

        public RangeAttack(int damage, float attackSpeed, float distance)
        {
            _damage = damage;
            _attackSpeed = attackSpeed;
            _distance = distance;
        }

        public ref float AttackSpeed => ref _attackSpeed;
        public ref float Damage =>  ref _damage;
        public float Distance => _distance;
        public AttackType AttackType => AttackType.Range;
        
        public void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(AttackType, _damage);
        }
    }
}