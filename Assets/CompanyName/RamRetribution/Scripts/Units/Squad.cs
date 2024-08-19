using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.SkillsModule.Intefaces;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad
    {
        private readonly int _maxMembers;
        private readonly List<Unit> _units;
        
        public Squad(int maxMembers)
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
            unit.Fleeing += Remove;
        }

        public void Remove(Unit unit)
        {
            if (_units == null || _units.Count <= 0)
                return;

            if (_units.Contains(unit))
            {
                if(unit is IBuffHolder holder)
                    holder.DeactivateBuff(_units);
                
                _units.Remove(unit);
            }
            else
                throw new ArgumentException(
                    $"Unit {unit.Type} is not listed in squad, but you trying to delete it");
        }

        #endregion

        public void OnComplete(int levelNumber)
        {
            foreach (var unit in _units)
                if (unit is IBuffHolder holder)
                    holder.ActivateBuff(_units,levelNumber);
        }

        public void OnLevelPassed(int nextLevelNumber)
        {
            foreach (var unit in _units)
            {
                if (unit is IBuffHolder holder)
                {
                    holder.DeactivateBuff(_units);
                    holder.ActivateBuff(_units, nextLevelNumber);
                }
            }
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