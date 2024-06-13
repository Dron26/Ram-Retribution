using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Units
{
    public class Squad
    {
        private List<Unit> _units;
        
        public void Add(Unit unit)
        {
            _units ??= new List<Unit>();

            if (_units.Count == 0)
            {
                _units.Add(unit);
                return;
            }

            if (unit.Type != _units[0].Type)
                throw new ArgumentException(
                    $"Cannot add unit with type {unit.Type} to squad with type {_units[0].Type}");

            _units.Add(unit);
        }

        public void Remove(Unit unit)
        {
            if (_units != null && _units.Count > 0)
                if (_units.Contains(unit))
                    _units.Remove(unit);
                else
                    throw new ArgumentException($"Unit {unit.Type} is not listed in squad, but you trying to delete it");
        }

        public void Move(Vector3 destination, Action callback = null)
        {
            foreach (var unit in _units)
            {
                unit.Move(destination, callback);
            }
        }

        public void Attack(IDamageable damageable)
        {
            foreach (var unit in _units)
            {
                unit.Attack(damageable);
            }
        }

        public void TakeDamage(AttackType type, int amount)
        {
            foreach (var unit in _units)
            {
                unit.TakeDamage(type, amount);
            }
        }

        public void Heal(int amount)
        {
            foreach (var unit in _units)
            {
                unit.Heal(amount);
            }
        }
    }
}