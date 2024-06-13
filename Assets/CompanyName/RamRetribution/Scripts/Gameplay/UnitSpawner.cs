using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Common.Services;
using CompanyName.RamRetribution.Scripts.Common.Visitors;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _ramsSpawnPoint;
        [SerializeField] private Transform _ramsContainer;
        [SerializeField] private Transform _enemiesContainer;
        
        private Vector3 _enemiesSpawnPoint = Vector3.zero;
        private Leader _leader;
        private Squad _ramsSquad;
        private Squad _enemiesSquad;

        private ConfigsContainer _configsContainer;
        private Dictionary<UnitTypes, IPlacementStrategy> _placementStrategies;
        private SetPositionVisitor _setPositionVisitor;
        private int _maxUnits;

        private IUnitFactory _factory;

        public Squad RamsSquad => _ramsSquad;
        public Squad EnemiesSquad => _enemiesSquad;

        public void Init(IUnitFactory factory, ConfigsContainer configsContainer)
        {
            _factory = factory;
            _configsContainer = configsContainer;
            _ramsSquad = new Squad();
            _enemiesSquad = new Squad();
        }

        public void Spawn(string id)
        {
            var config = _configsContainer.GetConfig(id);
            var unit = _factory.Create(config);

            switch (unit.Type)
            {
                case UnitTypes.Ram:
                    _ramsSquad.Add(unit);
                    unit.transform.SetParent(_ramsContainer);
                    SetPosition(unit);
                    break;
                case UnitTypes.Enemy:
                    _enemiesSquad.Add(unit);
                    unit.transform.SetParent(_enemiesContainer);
                    SetPosition(unit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetFactory(IUnitFactory factory)
        {
            _factory = factory;
        }

        public void AddPlacementStrategy<TStrategy>(UnitTypes forType, TStrategy placementStrategy)
            where TStrategy : IPlacementStrategy
        {
            _placementStrategies ??= new Dictionary<UnitTypes, IPlacementStrategy>();
            
            if (!_placementStrategies.TryAdd(forType, placementStrategy))
                _placementStrategies[forType] = placementStrategy;
        }

        private void SetPosition(Unit unit)
        {
            _setPositionVisitor ??= new SetPositionVisitor(
                _placementStrategies[UnitTypes.Ram], 
                _placementStrategies[UnitTypes.Enemy],
                _ramsSpawnPoint.position,
                _enemiesSpawnPoint);
            
            _setPositionVisitor.Visit(unit);
        }
    }
}