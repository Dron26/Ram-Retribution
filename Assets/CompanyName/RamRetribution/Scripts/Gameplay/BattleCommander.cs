using System;
using System.Collections.Generic;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompanyName.RamRetribution.Scripts.Gameplay
{
    [RequireComponent(typeof(EnemyConfigsHolder))]
    public class BattleCommander : MonoBehaviour
    {
        private UnitSpawner _unitSpawner;
        private List<UnitConfig> _enemiesConfigs;
        private StateMachine _levelStateMachine;
        //private LevelBuilder _levelBuilder;

        private Gate _gate;
        
        private Squad _ramsSquad;
        private Squad _enimiesSquad;

        private void Awake()
        {
            _gate = FindObjectOfType<Gate>();
            
            var configsHolder = GetComponent<EnemyConfigsHolder>();
            _enemiesConfigs = configsHolder.Get();

            _levelStateMachine = new StateMachine();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _unitSpawner.Spawn(_enemiesConfigs[Random.Range(0, _enemiesConfigs.Count)].Id);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _ramsSquad.Heal(10);
            }
        }

        public void Init(UnitSpawner spawner)
        {
            _unitSpawner = spawner;
            _ramsSquad = _unitSpawner.RamsSquad;
            
            var preBattleState = new PreBattleState(_ramsSquad, _gate);
            Any(preBattleState,null);
            
            _levelStateMachine.SetState<PreBattleState>();
        }

        private void At(IState fromState, IState toState, IPredicate condition)
            => _levelStateMachine.AddTransition(fromState, toState, condition);

        private void Any(IState toState, IPredicate condition)
            => _levelStateMachine.AddAnyTransition(toState, condition);
    }
}