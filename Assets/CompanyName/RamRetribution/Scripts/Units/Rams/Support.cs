using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Interfaces;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Support : Ram, IBuffHolder
    {
        private float _percentOfHealing = 0.1f;

        public override void Accept(IRamsVisitor visitor) 
            => visitor.Visit(this);

        public void ActivateBuff(List<Unit> units, int levelNumber)
        {
            
        }

        public void DeactivateBuff(List<Unit> units, int levelNumber)
        {
            
        }
        
        public override void AddBuff(BuffData buffData)
        {
            throw new System.NotImplementedException();
        }
    }
}