using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.Predicates;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    public class BattleCommander : MonoBehaviour
    {
        private const int TimeToCreateEnemies = 10;

        [SerializeField] private Transform _ramsStartPoint;
        
        private UnitSpawner _unitSpawner;
        private StateMachine _levelStateMachine;
        private LevelBuilder _levelBuilder;

        private CooldownTimer _countDownTimer;
        
        private void Update()
        {
            _levelStateMachine?.Update(Time.deltaTime);
            _countDownTimer?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _levelBuilder.GateAttackedFirst -= OnGateAttackedFirst;
        }

        public void Init(UnitSpawner spawner, LevelBuilder levelBuilder)
        {
            _unitSpawner = spawner;
            _levelBuilder = levelBuilder;
            
            _unitSpawner.CreateRamsSquad();
            
            _levelBuilder.GateAttackedFirst += OnGateAttackedFirst;
            
            _levelStateMachine = new StateMachine();
        
            var buildState = new BuildLevelState(_levelBuilder, _unitSpawner, _ramsStartPoint);
            var gateBattleState = new GateBattleState(_levelBuilder, _unitSpawner.Squads);
            var squadBattleState = new SquadBattleState(_unitSpawner.Squads);
        
            At(buildState,gateBattleState, new FuncPredicate(() => _levelBuilder.IsBuild));
            At(gateBattleState, squadBattleState, new FuncPredicate(() => _unitSpawner.Squads.HasEnemies));
            At(squadBattleState, gateBattleState, new FuncPredicate(() => !_unitSpawner.Squads.HasEnemies));
            Any(buildState, new FuncPredicate(() => _levelBuilder.IsCurrentGateDestroyed));
            
            _levelStateMachine.SetState<BuildLevelState>();
        }

        private void OnGateAttackedFirst()
        {
            _countDownTimer = new CooldownTimer(TimeToCreateEnemies);
            _countDownTimer.Start();
            _countDownTimer.OnTimerStop += SpawnEnemiesByTime;
        }

        private void SpawnEnemiesByTime()
        {
            _unitSpawner.SetSpawnPoints(_levelBuilder.EnemySpots);
            
            var enemiesId = new List<string>()
            {
                "LightEnemy",
                "LightEnemy",
                "LightEnemy",
                "LightEnemy",
                "LightEnemy",
            };
            
            _unitSpawner.Spawn(enemiesId);
            
            _countDownTimer.Reset();
            _countDownTimer.Start();
        }
        
        private void At(IState fromState, IState toState, IPredicate condition)
            => _levelStateMachine.AddTransition(fromState, toState, condition);

        private void Any(IState toState, IPredicate condition)
            => _levelStateMachine.AddAnyTransition(toState, condition);
    }
}