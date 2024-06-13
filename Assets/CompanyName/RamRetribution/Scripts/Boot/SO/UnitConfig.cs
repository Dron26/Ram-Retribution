using System;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot.SO
{
    [CreateAssetMenu(menuName = "UnitsConfig", order = 51)]
    public class UnitConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Unit Prefab { get; private set; }
        
        [Header("Health configuration")] 
        [SerializeField] private int _healthValue;
        [field: SerializeField] public ArmorTypes ArmorType { get; private set; }
        [field: SerializeField] public int ArmorValue { get; private set; }
        [field: SerializeField] public int ArmorReduceCoefficient { get; private set; }

        [Header("Attack configuration")] 
        [SerializeField] private int _damage;
        [field: SerializeField] public AttackType AttackType { get; private set; }
        [field: SerializeField] public float AttackSpeed { get; private set; }
        [field: SerializeField] public float AttackDistance { get; private set; }

        public int HealthValue => _healthValue;
        public int Damage => _damage;
    }
}