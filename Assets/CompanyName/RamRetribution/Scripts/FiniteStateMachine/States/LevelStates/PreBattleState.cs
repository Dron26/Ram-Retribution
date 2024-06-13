using CompanyName.RamRetribution.Scripts.Buildings;
using CompanyName.RamRetribution.Scripts.Units;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates
{
    public class PreBattleState : BaseState
    {
        private readonly Squad _ramsSquad;
        private readonly Gate _gate;
        
        public PreBattleState(Squad ramsSquad, Gate gate)
        {
            _ramsSquad = ramsSquad;
            _gate = gate;
        }
        
        public override void Enter()
        {
            _ramsSquad.Move(_gate.transform.position,OnGateReached);
            //Start Anim triangle move for Ram`s
            //Level builder.Build();
        }

        public override void Exit()
        {
            
        }

        private void OnGateReached()
        {
            _ramsSquad.Attack(_gate.Damageable);
        }
    }
}