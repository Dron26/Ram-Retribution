using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad<T>
        where T : Unit
    {
        private readonly int _maxMembers;
        private readonly IPlacementStrategy _placementStrategy;
        private readonly List<T> _units;
        private readonly List<IBuffHolder> _buffHolders;

        public Squad(int maxMembers, IPlacementStrategy placementStrategy)
        {
            _maxMembers = maxMembers;
            _placementStrategy = placementStrategy;
            _units = new List<T>();
            _buffHolders = new List<IBuffHolder>();
        }

        public bool IsAlive => _units.Count > 0;
        public IReadOnlyList<T> Units => _units;

        public void Add(T unit)
        {
            Validate(unit);

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
                tasks.Add(unit.MoveToPoint(at, token));

                await UniTask.Delay(
                    TimeSpan.FromSeconds(1.2f),
                    DelayType.Realtime,
                    cancellationToken: token);

                tasks.Add(unit.MoveToPoint(
                    _placementStrategy.SetPosition(at), 
                    token,
                    callback: unit.ActivateAgent));
            }

            await UniTask.WhenAll(tasks).WithCancellation(token);
            
            /*var movePointsCountPerUnit = _units.Count * 2;
            var tasks = new UniTask<bool>[_units.Count];

            for (var i = 0; i < _units.Count; i++)
            {
                tasks[i] = _units[i].MoveToPoint(at);

                await UniTask.Delay(
                        TimeSpan.FromSeconds(1.2f),
                        DelayType.Realtime)
                    .WithCancellation(tokenSource.Token);
            }

            await UniTask.WhenAll(tasks).WithCancellation(tokenSource.Token);

            foreach (var unit in _units)
                await unit.MoveToPoint(placementStrategy.SetPosition(at, unit), callback: unit.ActivateAgent);*/

            return true;
        }

        public void OnComplete(int levelNumber)
            => TryApplyBuffs(levelNumber);

        public void OnLevelPassed(int nextLevelNumber)
            => TryApplyBuffs(nextLevelNumber);

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

        private void Validate(T unit)
        {
            if (_units.Count > 0 && unit.GetType() != typeof(T))
                throw new ArgumentException(
                    $"Cannot add unit with type {unit} to squad with type {_units[0]}");

            if (_units.Count > _maxMembers || _maxMembers == 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}