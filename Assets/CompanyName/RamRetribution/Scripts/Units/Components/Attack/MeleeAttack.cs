using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class MeleeAttack : IAttackComponent
    {
        private const float AttackDistance = 5f;
        private float _damage;
        private float _attackSpeed;

        public MeleeAttack(int damage, float attackSpeed)
        {
            _damage = damage;
            _attackSpeed = attackSpeed;
        }

        public ref float AttackSpeed => ref _attackSpeed;
        public ref float Damage => ref _damage;
        public float Distance => AttackDistance;

        public void Attack(IAttackble entity) 
            => entity.Damageable.TakeDamage(this, _damage);
    }
}