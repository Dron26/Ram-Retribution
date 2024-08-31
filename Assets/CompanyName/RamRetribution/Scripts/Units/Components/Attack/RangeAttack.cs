using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class RangeAttack : IAttackComponent
    {
        private float _damage;
        private float _attackSpeed;

        public RangeAttack(int damage, float attackSpeed, float distance)
        {
            _damage = damage;
            _attackSpeed = attackSpeed;
            Distance = distance;
        }

        public ref float AttackSpeed => ref _attackSpeed;
        public ref float Damage =>  ref _damage;
        public float Distance { get; }
        
        public void Attack(IAttackble entity) 
            => entity.Damageable.TakeDamage(this, _damage);
    }
}