using CompanyName.RamRetribution.Scripts.Gameplay.LevelBuild;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Attack
{
    public class MagicAttack : IAttackComponent
    {
        private readonly float _damage;
        private readonly LevelCombinator _levelCombinator;

        public MagicAttack(float baseDamage, LevelCombinator levelCombinator)
        {
            _damage = baseDamage;
            _levelCombinator = levelCombinator;
        }
        
        public ref float AttackSpeed 
            => throw new System.InvalidOperationException("AttackSpeed is not implemented in this class.");
        public ref float Damage 
            => throw new System.InvalidOperationException("Damage is not implemented in this class.");

        public float Distance { get; }
        
        public void Attack(IAttackble entity)
        {
            entity.Damageable.TakeDamage(this, _damage * _levelCombinator.GetSpellDamage());
        }
    }
}