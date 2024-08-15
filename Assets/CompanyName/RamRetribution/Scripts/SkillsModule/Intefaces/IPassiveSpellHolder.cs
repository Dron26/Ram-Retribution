using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Skills.Intefaces
{
    public interface IPassiveSpellHolder
    {
        public void ActivatePassiveSkill(List<Unit> units);
        public void DeactivatePassiveSkill(List<Unit> units);
    }
}