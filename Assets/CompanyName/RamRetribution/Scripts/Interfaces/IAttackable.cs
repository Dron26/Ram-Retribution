using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IAttackable
    {
        public IDamageable Damageable { get; }
        public Transform SelfTransform { get; }
        public bool IsActive { get; }
    }
}