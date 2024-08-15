using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Skills.Intefaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public class Tank : Unit, IRam, IPassiveSpellHolder
    {
        public override UnitTypes Type => UnitTypes.Ram;

        public override void Accept(IUnitVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Unit Instance { get; }
        
        public void ActivatePassiveSkill(List<Unit> units)
        {
            
        }

        public void DeactivatePassiveSkill(List<Unit> units)
        {
            
        }
        
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
                        ram.Instance.TryGetComponent(out IAttackComponent attackComponnent);
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
    }
}