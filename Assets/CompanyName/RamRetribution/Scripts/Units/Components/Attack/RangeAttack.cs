using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class RangeAttack : IAttackComponent
    {
        private readonly int _damage;
        private readonly float _distance;

        public RangeAttack(int damage, float attackSpeed, float distance)
        {
            _damage = damage;
            AttackSpeed = attackSpeed;
            _distance = distance;
        }
        
        public float AttackSpeed { get; }
        public int Damage => _damage;
        public float Distance => _distance;
        public AttackType AttackType => AttackType.Range;
        
        public void Attack(IDamageable damageable)
        {
            
        }
    }
}