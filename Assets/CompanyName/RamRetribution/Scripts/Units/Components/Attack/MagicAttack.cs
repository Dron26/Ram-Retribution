using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class MagicAttack : IAttackComponent
    {
        private readonly float _damage;
        private readonly LvlCombinator _lvlCombinator;

        public MagicAttack(float baseDamage, LvlCombinator lvlCombinator)
        {
            _damage = baseDamage;
            _lvlCombinator = lvlCombinator;
        }
        
        public ref float AttackSpeed 
            => throw new System.InvalidOperationException("AttackSpeed is not implemented in this class.");
        public ref float Damage 
            => throw new System.InvalidOperationException("Damage is not implemented in this class.");

        public float Distance { get; }
        
        public void Attack(IDamageable damageable)
        {
            damageable.TakeDamage(this, _damage * _lvlCombinator.GetSpellDamage());
        }
    }
}