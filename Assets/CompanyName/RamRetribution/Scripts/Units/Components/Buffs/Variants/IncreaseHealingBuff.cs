using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Variants
{
    public class IncreaseHealingBuff : IBuff<IDamageable>
    {
        private const int Bonus = 2;
        
        public void Apply(IDamageable entity)
        {
            entity.Improve(Bonus);
        }
    }
}