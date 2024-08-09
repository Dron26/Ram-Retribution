using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using System.Collections;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Data;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Attacker : Unit, IRam, IPassiveSpellHolder
    {
        private readonly WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _spellCoroutine;
        private IBuff<IAttackComponent> _buff;
        
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
                Debug.Log("Heal started");
                var results = new Collider[9];
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << GameConstants.FriendlyLayerMask);
                IAttackComponent[] attackComponents = new IAttackComponent[9];
                int index = 0;
                foreach (var friens in results)
                {
                    if (friens.TryGetComponent(out IRam ram))
                    {
                        ram.GameObject.TryGetComponent(out IAttackComponent attackComponnent);
                        //attackComponnent.Damage += 1; Он доступен только для чтения. Надо его както менять так чтобы тебя не наругали
                        attackComponents[index] = attackComponnent;
                        index++;
                    }
                }
                yield return _coroutineDelay;
                for (int i = 0; i < index; i++)
                {
                    //attackComponents[i].Damage -= 1; Он доступен только для чтения.Надо его както менять так чтобы тебя не наругали
                }
            }
        }

        public void ActivatePassiveSkill()
        {
            _spellCoroutine = StartCoroutine(CheckRamsNearByForIncreaseAttackCoroutine());
        }

        public void DeactivatePassiveSkill()
        {
            StopCoroutine(_spellCoroutine);
        }

        private void OnDisable()
        {
            DeactivatePassiveSkill();
        }
    }
}