using System;
using UnityEngine;
using UnityEngine.AI;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Action _completeAction;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            enabled = false;
        }

        private void Update()
        {
            if (_agent.remainingDistance < _agent.stoppingDistance + float.Epsilon)
            {
                _completeAction?.Invoke();
                _completeAction = null;

                enabled = false;
            }
        }
        
        public AIMovement Move(Vector3 destination)
        {
            _agent.ResetPath();
            _agent.SetDestination(destination);

            enabled = true;
            
            return this;
        }

        public void OnComplete(Action callback)
        {
            _completeAction = callback;
        }
    }
}