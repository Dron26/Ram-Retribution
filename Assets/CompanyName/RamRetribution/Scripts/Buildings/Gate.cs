using System;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units.Components.Health;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Buildings
{
    public class Gate : MonoBehaviour
    {
        private IDamageable _damageable;

        public IDamageable Damageable => _damageable; 

        private void Awake()
        {
            _damageable = new GateHealth(100);
        }
    }
}