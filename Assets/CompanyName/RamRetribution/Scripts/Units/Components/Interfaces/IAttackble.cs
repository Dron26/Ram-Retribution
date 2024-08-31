using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Interfaces
{
    public interface IAttackble
    {
        public IDamageable Damageable { get; }
        public Transform SelfTransform { get; }
        public bool IsActive { get; }
    }
}