using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CompanyName.BehaviorsTest
{
    public class NavMeshMove : EnemyAction
    {
        public SharedVector3 Target;
        
        public override void OnStart()
        {
            if (!Agent.enabled)
                Agent.enabled = true;
            
            Agent.SetDestination(Target.Value);
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log(Agent.remainingDistance);
            
            return Agent.remainingDistance <= Agent.stoppingDistance 
                ? TaskStatus.Success 
                : TaskStatus.Running;
        }
    }
}