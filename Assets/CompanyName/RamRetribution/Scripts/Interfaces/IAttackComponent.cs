using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IAttackComponent
    {
        public AttackType AttackType { get; }
        public void Attack(IDamageable damageable);
    }
}