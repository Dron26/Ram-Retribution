using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Visitors.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using Cysharp.Threading.Tasks;

namespace CompanyName.RamRetribution.Scripts.Units.Rams
{
    public abstract class Ram : Unit
    {
        public new event Action<Ram> Fleeing;

        public abstract void Accept(IRamsVisitor visitor);
        public abstract void AddBuff(BuffData buffData);
        
        public void NotifyFindTarget(Dictionary<int, List<Enemy>> targetsByPriority)
        {
            CancelToken();

            FindTarget(
                targetsByPriority,
                PriorityTypes.High,
                PriorityTypes.Medium,
                PriorityTypes.Small);
        }
        
        private void FindTarget(Dictionary<int, List<Enemy>> targetsByPriority,
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

        protected override void OnHealthEnded(IDamageable damageable)
        {
            Fleeing?.Invoke(this);
            base.OnHealthEnded(damageable);
        }
    }
}