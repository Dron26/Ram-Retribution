using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDamageable : IImprovable
    {
        public event Action HealthEnded;
        public event Action<int> ValueChanged;

        public int Value { get; }
        public float ArmorValue { get; }

        public void TakeDamage(AttackType type, int damage);
        public void Restore(int amount);
    }
}