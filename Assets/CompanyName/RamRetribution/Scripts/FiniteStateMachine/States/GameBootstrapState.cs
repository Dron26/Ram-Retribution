using CompanyName.RamRetribution.Scripts.Common;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States
{
    public class GameBootstrapState : BaseState
    {
        private readonly StateMachine _stateMachine;

        public GameBootstrapState(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public override void Enter()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneNames.Gameplay);
            asyncOperation.completed += _ => LoadUnits();
        }

        private void LoadUnits()
        {
            UnitFactory unitFactory = new UnitFactory();
            
            Attacker attacker = unitFactory.Create(
                    UnitTypes.Attacker, 
                    new Vector3(5.99f, 2f, -0.399f)) 
                as Attacker;
        }
    }
}