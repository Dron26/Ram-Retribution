using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleMediator
    {
        private readonly List<Unit> _aliveRams = new();
        private UnitSpawner _unitSpawner;

        private readonly Dictionary<int, List<Unit>> _enemiesToAttack = new()
        {
            { (int)PriorityTypes.Leader, new List<Unit>() },
            { (int)PriorityTypes.Small, new List<Unit>() },
            { (int)PriorityTypes.Medium, new List<Unit>() },
            { (int)PriorityTypes.High, new List<Unit>() },
        };

        public void RegisterSpawner(UnitSpawner spawner)
        {
            _unitSpawner = spawner;

            _unitSpawner.RamCreated += AddRam;
            _unitSpawner.EnemiesCreated += NotifyToAttack;
        }

        public void UnRegisterSpawner()
        {
            if(_unitSpawner == null) return;

            _unitSpawner.RamCreated -= AddRam;
            _unitSpawner.EnemiesCreated -= NotifyToAttack;
        }
        
        private void AddRam(Unit unit)
        {
            _aliveRams.Add(unit);
            unit.Fleeing += OnRamFleeing;
        }

        private void NotifyToAttack(List<Unit> enemies)
        {
            foreach (var enemy in enemies)
            {
                _enemiesToAttack[(int)enemy.Priority].Add(enemy);
                enemy.Fleeing += OnEnemyFleeing;
            }

            foreach (var ram in _aliveRams)
            {
                ram.FindTarget(_enemiesToAttack);
            }
        }
        
        private void OnRamFleeing(Unit ram)
        {
            ram.Fleeing -= OnRamFleeing;
            ram.MyAttackerWaitingCommand += NotifyToAttack;
            _aliveRams.Remove(ram);
        }

        private void OnEnemyFleeing(Unit enemy)
        {
            enemy.Fleeing -= OnEnemyFleeing;
            enemy.MyAttackerWaitingCommand += NotifyToAttack;
            _enemiesToAttack[(int)enemy.Priority].Remove(enemy);
        }
    }
}