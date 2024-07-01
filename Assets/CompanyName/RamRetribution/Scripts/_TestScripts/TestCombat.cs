using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts._TestScripts
{
    public class TestCombat : MonoBehaviour
    {
        private UnitSpawner _unitSpawner;
        private LevelBuilder _levelBuilder;
        private Gate _gate;
        private IReadOnlyList<Unit> _rams = new List<Unit>();

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(_rams == null || _gate == null) return;
                
                foreach (var ram in _rams)
                {
                    ram.AttackGate(_gate);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                if(_rams == null) return;
                
                _unitSpawner.SpawnEnemies(new List<string>()
                {
                    "LightEnemy",
                    "LightEnemy",
                    "LightEnemy",
                    "LightEnemy",
                });
            }
        }

        public void Init(LevelBuilder levelBuilder, UnitSpawner unitSpawner)
        {
            _levelBuilder = levelBuilder;
            _unitSpawner = unitSpawner;
            
            _levelBuilder.Build();
            
            _gate = levelBuilder.CurrentGate;
            _unitSpawner.CreateRamsSquad();
            _unitSpawner.SetEnemiesSpawnPoints(_levelBuilder.EnemySpots);
            
            _rams = unitSpawner.Squads.Rams.Units;
        }
    }
}