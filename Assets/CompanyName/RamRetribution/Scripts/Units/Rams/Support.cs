using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using System.Collections;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Support : Unit, IRam, IPassiveSpellHolder
    {
        private int FriendlyLayerMask = 8;
        private float _percentOfHealing = 0.1f;
        private WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;


        public override UnitTypes Type => UnitTypes.Ram;

        public GameObject GameObject => gameObject;

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
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << FriendlyLayerMask);

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

        public void ActivatePassiveSkill()
        {
            _cachedCoroutine = StartCoroutine(CheckRamsNearByForHealingCoroutine());
        }

        public void DeactivatePassiveSkill()
        {
            StopCoroutine(_cachedCoroutine);
        }

        private void OnDisable()
        {
            DeactivatePassiveSkill();
        }
    }
}