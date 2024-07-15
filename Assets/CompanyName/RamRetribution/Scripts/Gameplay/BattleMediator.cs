using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleMediator
    {
        private readonly Dictionary<int, List<Unit>> _aliveRams = new()
        {
            { (int)PriorityTypes.Leader, new List<Unit>() },
            { (int)PriorityTypes.Small, new List<Unit>() },
            { (int)PriorityTypes.Medium, new List<Unit>() },
            { (int)PriorityTypes.High, new List<Unit>() },
        };

        private readonly Dictionary<int, List<Unit>> _enemiesToAttack = new()
        {
            { (int)PriorityTypes.Leader, new List<Unit>() },
            { (int)PriorityTypes.Small, new List<Unit>() },
            { (int)PriorityTypes.Medium, new List<Unit>() },
            { (int)PriorityTypes.High, new List<Unit>() },
        };

        private UnitSpawner _unitSpawner;

        public void RegisterSpawner(UnitSpawner spawner)
        {
            _unitSpawner = spawner;

            _unitSpawner.RamsCreated += AddRams;
            _unitSpawner.EnemiesCreated += AddEnemies;
        }

        public void UnRegisterSpawner()
        {
            if (_unitSpawner == null) return;

            _unitSpawner.RamsCreated -= AddRams;
            _unitSpawner.EnemiesCreated -= AddEnemies;
        }

        private void AddRams(IReadOnlyList<Unit> rams)
        {
            foreach (var ram in rams)
            {
                _aliveRams[(int)ram.Priority].Add(ram);
                ram.Fleeing += OnRamFleeing;
            }
        }

        private void AddEnemies(IReadOnlyList<Unit> enemies)
        {
            foreach (var enemy in enemies)
            {
                _enemiesToAttack[(int)enemy.Priority].Add(enemy);
                enemy.Fleeing += OnEnemyFleeing;
            }
            
            NotifyEnemies(enemies);
            NotifyRams();
        }
        
        private void NotifyRams()
        {
            foreach (var rams in _aliveRams.Values)
                for (var index = 0; index < rams.Count; index++)
                {
                    var ram = rams[index];
                    ram.NotifyFindTarget(_enemiesToAttack);
                }
        }

        private void NotifyEnemies(IReadOnlyList<Unit> enemies)
        {
            for (var index = 0; index < enemies.Count; index++)
            {
                var enemy = enemies[index];
                enemy.NotifyFindTarget(_aliveRams);
            }
        }

        private void OnRamFleeing(Unit ram)
        {
            _aliveRams[(int)ram.Priority].Remove(ram);
            ram.Fleeing -= OnRamFleeing;
            
            NotifyEnemies(ram.CurrentEnemies);
        }

        private void OnEnemyFleeing(Unit enemy)
        {
            _enemiesToAttack[(int)enemy.Priority].Remove(enemy);
            enemy.Fleeing -= OnEnemyFleeing;
            
            NotifyRams();
        }
    }
}