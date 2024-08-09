using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Demolisher : Unit, IRam
    {
        private readonly WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;
        private IBuff _buff;

        public override UnitTypes Type => UnitTypes.Ram;
        public GameObject GameObject => gameObject;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        private IEnumerator CheckRamsNearByForIncreaseAttackCoroutine()
        {
            while (true)
            {
                Debug.Log(" Add GateDamage started");
                var results = new Collider[9];
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << GameConstants.FriendlyLayerMask);
                IAttackComponent[] attackComponents = new IAttackComponent[9];
                int index = 0;
                
                foreach (var ally in results)
                {
                    if (ally.TryGetComponent(out IRam ram))
                    {
                        ram.GameObject.TryGetComponent(out IAttackComponent attackComponent);
                        //attackComponent.GateDamage += 1; Он доступен только для чтения. Надо его както менять так чтобы тебя не наругали
                        attackComponents[index] = attackComponent;
                        index++;
                    }
                }
                yield return _coroutineDelay;
                
                for (int i = 0; i < index; i++)
                {
                    //attackComponents[i].GateDamage -= 1; Он доступен только для чтения.Надо его както менять так чтобы тебя не наругали
                }
            }
        }

        public void ActivatePassiveSkill()
        {
            _cachedCoroutine = StartCoroutine(CheckRamsNearByForIncreaseAttackCoroutine());
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