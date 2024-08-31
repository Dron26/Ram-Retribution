using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CompanyName.BehaviorsTest
{
    public class WaypointsPath : EnemyAction
    {
        public SharedTransformList Waypoints;

        private int _currentPointIndex;

        public override void OnStart()
        {
            _currentPointIndex = 0;
        }

        public override TaskStatus OnUpdate()
        {
            if (_currentPointIndex >= Waypoints.Value.Count)
                return TaskStatus.Success;

            transform.position = Vector3.MoveTowards(transform.position, Waypoints.Value[_currentPointIndex].position,
                Agent.speed * Time.deltaTime);

            if ((Waypoints.Value[_currentPointIndex].position - transform.position).sqrMagnitude <= Agent.stoppingDistance)
                _currentPointIndex++;

            return TaskStatus.Running;
        }
    }
}