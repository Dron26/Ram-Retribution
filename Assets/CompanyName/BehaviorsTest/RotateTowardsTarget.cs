using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CompanyName.BehaviorsTest
{
    public class RotateTowardsTarget : Action
    {
        public SharedVector3 Target;

        private float _speed = 25f;

        public override TaskStatus OnUpdate()
        {
            var direction = (Target.Value - transform.position).normalized;

            if (direction.magnitude > 0.1f)
                return TaskStatus.Success;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);

            return TaskStatus.Running;
        }
    }
}