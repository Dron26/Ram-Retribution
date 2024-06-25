using CompanyName.RamRetribution.Scripts.Gameplay;
using CompanyName.RamRetribution.Scripts.Units;

namespace CompanyName.RamRetribution.Scripts.FiniteStateMachine.States.LevelStates
{
    public class SquadBattleState : BaseState
    {
        private readonly SquadsHolder _squadsHolder;
        
        public SquadBattleState(SquadsHolder squadsHolder)
        {
            _squadsHolder = squadsHolder;
        }
        
        public override void Enter()
        {
            
        }
        
        public override void Update(float deltaTime)
        {

        }
    }
}