using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Demolisher : Unit, IRam
    {
        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }
        private WaitForSeconds _coroutineDelay = new WaitForSeconds(2);
        private Coroutine _cachedCoroutine;
        private int _friendlyLayerMask = 8;

        public GameObject GameObject => gameObject;

        private IEnumerator CheckRamsNearByForIncreaseAttackCoroutine()
        {
            while (true)
            {
                Debug.Log(" Add GateDamage started");
                var results = new Collider[9];
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << _friendlyLayerMask);
                IAttackComponent[] attackComponents = new IAttackComponent[9];
                int index = 0;
                foreach (var friens in results)
                {
                    if (friens.TryGetComponent(out IRam ram))
                    {
                        ram.GameObject.TryGetComponent(out IAttackComponent attackComponnent);
                        //attackComponnent.GateDamage += 1; �� �������� ������ ��� ������. ���� ��� ����� ������ ��� ����� ���� �� ��������
                        attackComponents[index] = attackComponnent;
                        index++;
                    }
                }
                yield return _coroutineDelay;
                for (int i = 0; i < index; i++)
                {
                    //attackComponents[i].GateDamage -= 1; �� �������� ������ ��� ������.���� ��� ����� ������ ��� ����� ���� �� ��������
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