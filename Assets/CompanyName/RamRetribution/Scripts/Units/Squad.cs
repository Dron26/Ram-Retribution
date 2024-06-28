using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad
    {
        private readonly int _maxMembers;
        private readonly IPlacementStrategy _placementStrategy;
        private readonly UsiCalculator _usiCalculator;
        
        private List<Unit> _units;
        private Dictionary<int, List<Unit>> _unitsDict = new();

        private Transform _origin;
        private UnitsPlacementVisitor _placementVisitor;
        
        public IReadOnlyList<Unit> Units => _units;
        public int TotalUsi { get; private set; }
        public bool IsAlive => _units.Count > 0;
        public bool IsEmpty => _units.Count == 0;
        
        //add bool IsActive => all units.IsActive = true;

        public Squad (int maxMembers, IPlacementStrategy placementStrategy)
        {
            _maxMembers = maxMembers;
            _placementStrategy = placementStrategy;
            _units = new List<Unit>();
            _usiCalculator = new UsiCalculator();
            
            _unitsDict.Add(0, new List<Unit>());
            _unitsDict.Add(1, new List<Unit>());
            _unitsDict.Add(2, new List<Unit>());
            _unitsDict.Add(3, new List<Unit>());
        }

        #region AddRemove

        public void Add(Unit unit)
        {
            Validate(unit);

            //_units.Add(unit);
            _unitsDict[(int)unit.Priority].Add(unit);
            _units = _units.OrderByDescending(member => member.Priority).ToList();
            unit.Fleeing += OnUnitFleeing;
            TotalUsi += _usiCalculator.ConvertTo(unit);
        }
        
        private void Remove(Unit unit)
        {
            if (_units != null && _units.Count > 0)
                if (_units.Contains(unit))
                {
                    _units.Remove(unit);
                    TotalUsi -= _usiCalculator.ConvertTo(unit);
                }
                else
                    throw new ArgumentException(
                        $"Unit {unit.Type} is not listed in squad, but you trying to delete it");
        }

        #endregion

        #region OvverideMembers

        public void Move(Vector3 destination, Action callback = null)
        {
            var unitsInProgress = _units.Count;

            for (int i = 0; i < _units.Count; i++)
            {
                Vector3 offset = _units[i].transform.position - _origin.position;
                _units[i].MoveToPoint(destination + offset, () =>
                {
                    unitsInProgress--;

                    if (unitsInProgress == 0 && callback != null)
                        callback?.Invoke();
                });
            }
        }

        public void Heal(int amount)
        {
            foreach (var unit in _units)
            {
                unit.Heal(amount);
            }
        }

        #endregion
        
        public void SetOrigin(Transform origin)
        {
            _origin = origin;
            _placementVisitor = new UnitsPlacementVisitor(_origin.position, _placementStrategy);

            foreach (var unit in _units)
                _placementVisitor.Visit(unit);
        }

        public Dictionary<int, List<Unit>> GetTargets()
        {
            foreach (var unit in _units)
            {
                int priority = (int)unit.Priority;
                _unitsDict[priority].Add(unit);
            }

            return _unitsDict;
        }
        
        private void OnUnitFleeing(Unit unit)
        {
            unit.Fleeing -= OnUnitFleeing;
            _unitsDict[(int)unit.Priority].Remove(unit);
            Remove(unit);
            unit.SelfDestroy();
        }

        private void Validate(Unit unit)
        {
            if (_units.Count > 0 && unit.Type != _units[0].Type)
                throw new ArgumentException(
                    $"Cannot add unit with type {unit.Type} to squad with type {_units[0].Type}");

            if (_units.Count + 1 > _maxMembers || _maxMembers == 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}