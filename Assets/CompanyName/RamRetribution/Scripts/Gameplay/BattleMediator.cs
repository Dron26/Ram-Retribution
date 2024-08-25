using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;
using CompanyName.RamRetribution.Scripts.Units.Enemies;
using CompanyName.RamRetribution.Scripts.Units.Rams;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleMediator : IDisposable
    {
        private readonly UnitSpawner _unitSpawner;

        private readonly Dictionary<int, List<Ram>> _aliveRams = new()
        {
            { (int)PriorityTypes.Leader, new List<Ram>() },
            { (int)PriorityTypes.Small, new List<Ram>() },
            { (int)PriorityTypes.Medium, new List<Ram>() },
            { (int)PriorityTypes.High, new List<Ram>() },
        };

        private readonly Dictionary<int, List<Enemy>> _enemies = new()
        {
            { (int)PriorityTypes.Small, new List<Enemy>() },
            { (int)PriorityTypes.Medium, new List<Enemy>() },
            { (int)PriorityTypes.High, new List<Enemy>() },
        };

        public BattleMediator(UnitSpawner spawner)
        {
            _unitSpawner = spawner;

            _unitSpawner.RamsCreated += OnRamsCreated;
            _unitSpawner.EnemiesCreated += OnEnemiesCreated;
        }

        public void Dispose()
        {
            _unitSpawner.RamsCreated -= OnRamsCreated;
            _unitSpawner.EnemiesCreated -= OnEnemiesCreated;
        }

        private void OnRamsCreated(Squad<Ram> squad)
        {
            foreach (var ram in squad.Units)
            {
                _aliveRams[(int)ram.Priority].Add(ram);
                ram.Fleeing += OnRamFleeing;
            }
        }

        private void OnEnemiesCreated(Squad<Enemy> squad)
        {
            foreach (var enemy in squad.Units)
            {
                _enemies[(int)enemy.Priority].Add(enemy);
                enemy.Fleeing += OnEnemyFleeing;
            }

            NotifyEnemies();
            NotifyRams();
        }

        private void NotifyRams()
        {
            foreach (var rams in _aliveRams.Values)
                for (var index = 0; index < rams.Count; index++)
                {
                    var ram = rams[index];

                    if (ram is not Demolisher)
                        ram.NotifyFindTarget(_enemies);
                }
        }

        private void NotifyEnemies()
        {
            foreach (var enemies in _enemies.Values)
                for (var index = 0; index < enemies.Count; index++)
                {
                    var enemy = enemies[index];
                    enemy.NotifyFindTarget(_aliveRams);
                }
        }

        private void OnRamFleeing(Ram ram)
        {
            _aliveRams[(int)ram.Priority].Remove(ram);
            ram.Fleeing -= OnRamFleeing;

            NotifyEnemies();
        }

        private void OnEnemyFleeing(Enemy enemy)
        {
            _enemies[(int)enemy.Priority].Remove(enemy);
            enemy.Fleeing -= OnEnemyFleeing;

            NotifyRams();
        }
    }
}