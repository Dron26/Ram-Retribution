using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad
    {
        private readonly int _maxMembers;
        private readonly IPlacementStrategy _placementStrategy;
        
        private List<Unit> _units;

        private Transform _origin;

        public Squad (int maxMembers, IPlacementStrategy placementStrategy)
        {
            _maxMembers = maxMembers;
            _placementStrategy = placementStrategy;
            _units = new List<Unit>();
        }
        
        public IReadOnlyList<Unit> Units => _units;

        #region AddRemove

        public void Add(Unit unit)
        {
            Validate(unit);

            _units.Add(unit);
            _units = _units.OrderByDescending(member => member.Priority).ToList();
        }
        
        private void Remove(Unit unit)
        {
            if (_units == null || _units.Count <= 0)
                return;
            
            if (_units.Contains(unit))
                _units.Remove(unit);
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
        #endregion
        
        public void SetOrigin(Transform origin) 
            => _origin = origin;

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