using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IAttackComponent : IImprovable
    {
        public AttackType AttackType { get; }
        public float AttackSpeed { get; }
        public int Damage { get; }
        public float Distance { get; }
        public void Attack(IDamageable damageable);
    }
}