using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(AttackType type, int damage);
        public void Heal(int amount);
    }
}