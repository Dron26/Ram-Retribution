using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IAttackComponent : IImprovable
    {
        public AttackType AttackType { get; }
        public ref float AttackSpeed { get; }
        public ref float Damage { get; }
        public float Distance { get; }
        public void Attack(IDamageable damageable);
    }
}