using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompanyName.RamRetribution.Scripts.SkillsModule.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using CompanyName.RamRetribution.Scripts.Units.Components.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad<T>
        where T : Unit
    {
        private readonly int _maxMembers;
        private readonly List<T> _units;
        private readonly List<IBuffHolder> _buffHolders;
        
        private IPlacementStrategy _placementStrategy;

        public Squad(int maxMembers)
        {
            _maxMembers = maxMembers;
            _units = new List<T>();
            _buffHolders = new List<IBuffHolder>();
        }

        public bool IsAlive => _units.Count > 0;
        public IReadOnlyList<T> Units => _units;

        public void Add(T unit)
        {
            _units.Add(unit);

            if (unit is IBuffHolder holder)
                _buffHolders.Add(holder);

            unit.Fleeing += Remove;
        }

        public async UniTask<bool> MoveTo(Vector3 at, CancellationToken token = default)
        {
            var tasks = new List<UniTask<bool>>();

            foreach (var unit in _units)
            {
                unit.DeactivateAgent();
                tasks.Add(unit.TransformMoveToPointAsync(at, token));
                
                await UniTask.Delay(
                    TimeSpan.FromSeconds(1.5f),
                    DelayType.DeltaTime,
                    cancellationToken: token);
            }

            await UniTask.WhenAny(tasks).WithCancellation(token);
            
            tasks.Clear();
            
            foreach (var unit in _units)
            {
                tasks.Add(unit.TransformMoveToPointAsync(
                    _placementStrategy.SetPosition(at,unit),
                    token,
                    callback: unit.ActivateAgent));
            }

            await UniTask.WhenAll(tasks).WithCancellation(token);
            
            /*foreach (var unit in _units)
            {
                unit.DeactivateAgent();
                tasks.Add(unit.MoveToPoint(at, token));

                await UniTask.Delay(
                    TimeSpan.FromSeconds(1.2f),
                    DelayType.DeltaTime,
                    cancellationToken: token);

                tasks.Add(unit.MoveToPoint(
                    _placementStrategy.SetPosition(at,unit),
                    token,
                    callback: unit.ActivateAgent));
            }

            await UniTask.WhenAll(tasks).WithCancellation(token);*/

            return true;
        }

        public void Attack(IAttackble attackble)
        {
            foreach (var unit in _units)
                unit.Attack(attackble).Forget();
        }
        
        public void OnComplete(int levelNumber)
            => TryApplyBuffs(levelNumber);

        public void OnLevelPassed(int nextLevelNumber)
            => TryApplyBuffs(nextLevelNumber);

        public Squad<T> SetPlacementStrategy(IPlacementStrategy strategy)
        {
            _placementStrategy = strategy;
            return this;
        }

        private void Remove(Unit unit)
        {
            if (unit is not T typedUnit || !_units.Contains(typedUnit))
                throw new ArgumentException($"Unit {unit} is not in the squad.");

            _units.Remove(typedUnit);

            if (unit is IBuffHolder holder)
                _buffHolders.Remove(holder);
        }

        private void TryApplyBuffs(int levelNumber)
        {
            if (_buffHolders.Count == 0)
                return;

            var units = _units.Cast<Unit>().ToList();

            foreach (var holder in _buffHolders)
            {
                holder.DeactivateBuff(units, levelNumber);
                holder.ActivateBuff(units, levelNumber);
            }
        }
    }
}