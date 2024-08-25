using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Demolisher : Ram, IRam
    {
        private readonly WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;

        public override void Accept(IRamsVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void ActivatePassiveSkill()
        {
            
        }

        public void DeactivatePassiveSkill()
        {
            
        }
        
        public override void AddBuff(BuffData buffData)
        {
            throw new System.NotImplementedException();
        }
    }
}