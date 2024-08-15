using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Support : Unit, IRam, IPassiveSpellHolder
    {
        private float _percentOfHealing = 0.1f;
        private WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;

        public override UnitTypes Type => UnitTypes.Ram;

        public Unit Instance { get; }

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }

        private IEnumerator CheckRamsNearByForHealingCoroutine()
        {
            while (true)
            {
                Debug.Log(" Heal started");
                var results = new Collider[9];
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << GameConstants.FriendlyLayerMask);

                foreach (var friens in results)
                {
                    if (friens.TryGetComponent(out IRam ram))
                    {
                        //ram.Heal(_baseHealingValue);
                        //ram.Health += ram.Health * _percentOfHealing
                    }
                }
                
                yield return _coroutineDelay;
            }
        }

        public void ActivatePassiveSkill(List<Unit> units)
        {
            
        }

        public void DeactivatePassiveSkill(List<Unit> units)
        {
            
        }
    }
}