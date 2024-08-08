using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Tank : Unit, IRam
    {
        private WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;
        private int _friendlyLayerMask = 8;

        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }

        public GameObject GameObject => gameObject;

        private IEnumerator CheckRamsNearByForIncreaseAttackCoroutine()
        {
            while (true)
            {
                Debug.Log(" Add Armor started");
                var results = new Collider[9];
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << _friendlyLayerMask);
                IAttackComponent[] attackComponents = new IAttackComponent[9];
                int index = 0;
                foreach (var friens in results)
                {
                    if (friens.TryGetComponent(out IRam ram))
                    {
                        ram.GameObject.TryGetComponent(out IAttackComponent attackComponnent);
                        //attackComponnent.Armor += 1; ќн доступен только дл€ чтени€. Ќадо его както мен€ть так чтобы теб€ не наругали
                        attackComponents[index] = attackComponnent;
                        index++;
                    }
                }
                yield return _coroutineDelay;
                for (int i = 0; i < index; i++)
                {
                    //attackComponents[i].Armor -= 1; ќн доступен только дл€ чтени€.Ќадо его както мен€ть так чтобы теб€ не наругали
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