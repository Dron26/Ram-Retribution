using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Units.Components.Buffs.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Tank : Unit, IRam, IImprover
    {
        private IBuff _buff;

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
                Physics.OverlapSphereNonAlloc(transform.position, 10, results, 1 << GameConstants.FriendlyLayerMask);
                IAttackComponent[] attackComponents = new IAttackComponent[9];
                int index = 0;
                foreach (var friens in results)
                {
                    if (friens.TryGetComponent(out IRam ram))
                    {
                        ram.GameObject.TryGetComponent(out IAttackComponent attackComponnent);
                        //attackComponnent.Armor += 1; �� �������� ������ ��� ������. ���� ��� ����� ������ ��� ����� ���� �� ��������
                        attackComponents[index] = attackComponnent;
                        index++;
                    }
                }
                yield return null;
                for (int i = 0; i < index; i++)
                {
                    //attackComponents[i].Armor -= 1; �� �������� ������ ��� ������.���� ��� ����� ������ ��� ����� ���� �� ��������
                }
            }
        }

        public void AddBuff(List<Unit> units)
        {
            foreach (var unit in units)
                _buff.Apply(unit.Damageable);
        }

        public void RemoveBuff(List<Unit> units)
        {
            foreach (var unit in units)
                _buff.Apply(unit.Damageable);
        }
    }
}