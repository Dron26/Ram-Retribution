using System.Collections.Generic;

namespace CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces
{
    public interface IImprover
    {
        public void AddBuff(List<Unit> units);
        public void RemoveBuff(List<Unit> units);
    }
}