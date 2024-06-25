using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Interfaces
{
    public interface IDetectionStrategy
    {
        public bool Execute(Transform unit, Transform detector, CooldownTimer timer);
    }
}