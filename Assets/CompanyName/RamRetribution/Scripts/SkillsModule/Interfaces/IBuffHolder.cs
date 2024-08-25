using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Rams;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces
{
    public interface IBuffHolder
    {
        public void ActivateBuff(List<Unit> units, int currentLevelNumber);
        public void DeactivateBuff(List<Unit> units, int currentLevelNumber);
    }
}