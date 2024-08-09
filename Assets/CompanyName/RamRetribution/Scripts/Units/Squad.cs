using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad
    {
        private readonly int _maxMembers;
        
        private List<Unit> _units;
        private Transform _origin;

        public Squad (int maxMembers)
        {
            _maxMembers = maxMembers;
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