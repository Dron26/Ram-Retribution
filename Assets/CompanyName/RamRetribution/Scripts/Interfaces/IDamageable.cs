using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDamageable
    {
        public event Action HealthEnded;
        public event Action<int> ValueChanged;
        public int Health { get; }
        public void TakeDamage(AttackType type, int damage);
        public void Restore(int amount);
    }
}