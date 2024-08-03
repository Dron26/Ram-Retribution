using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IArmor
    {
        public float Value { get; }
        public int ReduceDamage(AttackType type ,float damage);
    }
}