using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Rams;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units.Enemies
{
    public abstract class Enemy : Unit
    {
        public new event Action<Enemy> Fleeing;
        public abstract CurrencyTypes RewardCurrency { get; }

        public void NotifyFindTarget(Dictionary<int, List<Ram>> targetsByPriority)
        {
            CancelToken();

            FindTarget(
                targetsByPriority,
                PriorityTypes.High,
                PriorityTypes.Medium,
                PriorityTypes.Small,
                PriorityTypes.Leader);
        }

        protected override void OnHealthEnded(IDamageable damageable)
        {
            Fleeing?.Invoke(this);
            base.OnHealthEnded(damageable);
        }

        private void FindTarget(Dictionary<int, List<Ram>> targetsByPriority,
            params PriorityTypes[] priorityTypesArray)
        {
            foreach (var priority in priorityTypesArray)
            {
                if (targetsByPriority[(int)priority].Count > 0)
                {
                    var unitWithFewerAttackers = targetsByPriority[(int)priority]
                        .OrderBy(unit => unit.CurrentEnemies.Count)
                        .First();

                    Fleeing += unitWithFewerAttackers.OnAttackersFleeing;
                    unitWithFewerAttackers.CurrentEnemies.Add(this);
                    Attack(unitWithFewerAttackers).Forget();

                    return;
                }
            }
        }
    }
}