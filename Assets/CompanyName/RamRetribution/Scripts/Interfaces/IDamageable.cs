using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDamageable : IImprovable
    {
        public event Action<IDamageable> HealthEnded;
        public event Action<float> ValueChanged;

        public float CurrentValue { get; }
        public ref float ArmorValue { get; }

        public void TakeDamage(IAttackComponent attackComponent, float damage);
        public void Heal(int amount);
    }
}