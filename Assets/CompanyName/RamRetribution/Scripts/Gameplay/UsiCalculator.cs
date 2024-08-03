using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    /// <summary>
    /// USI - Unified strength indicator.
    /// Represents a class that calculates and manages a unified strength indicator for a specific gameplay aspect like
    /// Unit or Squad of units.
    /// </summary>
    public class UsiCalculator
    {
        private const int HealthCost = 2;
        private const int DamageCost = 4;
        
        public int ConvertTo(Unit unit)
        {
            var healthUsi = unit.Damageable.Value / HealthCost;
            var damageUsi = unit.Damage / DamageCost;

            return healthUsi + damageUsi;
        }

        public int ConvertFrom(int usi, ConvertableStatsTypes type)
        {
            switch (type)
            {
                case ConvertableStatsTypes.Health:
                    return usi * HealthCost;
                case ConvertableStatsTypes.Damage:
                    return usi * DamageCost;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}