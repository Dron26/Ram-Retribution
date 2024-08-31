using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using CompanyName.RamRetribution.Scripts.Common;
using UnityEngine;

namespace CompanyName.BehaviorsTest
{
    public class MoveToPoint : EnemyAction
    {
        public SharedVector3 Target;

        public override void OnStart()
        {
            Animator.SetBool(AIAnimatorParams.Run, true);
        }

        public override TaskStatus OnUpdate()
        {
            if ((Target.Value - transform.position).sqrMagnitude <= Agent.stoppingDistance)
            {
                Animator.SetBool(AIAnimatorParams.Run, false);
                return TaskStatus.Success;
            }
            
            transform.position = Vector3.MoveTowards(
                transform.position, Target.Value, Agent.speed * Time.deltaTime);

            return TaskStatus.Running;
        }
    }
}