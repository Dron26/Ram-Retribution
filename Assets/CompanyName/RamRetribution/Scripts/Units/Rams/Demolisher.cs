using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Demolisher : Unit, IRam
    {
        private readonly WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;

        public override UnitTypes Type => UnitTypes.Ram;
        public Unit Instance { get; }

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void ActivatePassiveSkill()
        {
            
        }

        public void DeactivatePassiveSkill()
        {
            
        }

        private void OnDisable()
        {
            DeactivatePassiveSkill();
        }
    }
}