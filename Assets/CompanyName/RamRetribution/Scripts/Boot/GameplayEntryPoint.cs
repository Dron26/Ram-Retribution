using System.Collections;
using CompanyName.RamRetribution.Scripts.Boot.SO;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Factorys;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine;
using CompanyName.RamRetribution.Scripts.FiniteStateMachine.States;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Boot
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UnitDTO _unitDto;
        
        private IEnumerator Start()
        {
            var stateMachine = new StateMachine();
            stateMachine.AddAnyTransition(new GameBootstrapState(stateMachine), null);
            stateMachine.SetState(new GameBootstrapState(stateMachine));

            IFactory<Unit> unitFactory = new UnitFactory();
            Attacker attacker = unitFactory.Create(
                UnitTypes.Attacker, 
                new Vector3(5.99f, 2f, -0.399f)) 
                as Attacker;
            
            attacker.Init(_unitDto, 10);
            
            yield break;
        }
    }
}