using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CompanyName.BehaviorsTest
{
    public class SetTarget : Conditional
    {
        public SharedTransformList Waypoints;
        public SharedVector3 Target;

        private int _currentPointIndex = 0;

        public override TaskStatus OnUpdate()
        {
            if (_currentPointIndex >= Waypoints.Value.Count)
                return TaskStatus.Failure;

            Target.Value = Waypoints.Value[_currentPointIndex].position;
            _currentPointIndex++;
            
            return TaskStatus.Success;
        }
    }
}