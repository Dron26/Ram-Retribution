using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces
{
    public interface IBuffHolder
    {
        public void ActivateBuff(List<Unit> units, int currentLevelNumber);
        public void DeactivateBuff(List<Unit> units, int currentLevelNumber);
    }
}