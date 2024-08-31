using BehaviorDesigner.Runtime.Tasks;
using CompanyName.RamRetribution.Scripts.Units.Components;
using UnityEngine;
using UnityEngine.AI;

namespace CompanyName.BehaviorsTest
{
    public class EnemyAction : Action
    {
        protected NavMeshAgent Agent;
        protected AIMovement AIMovement;
        protected Animator Animator;

        public override void OnAwake()
        {
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
            AIMovement = GetComponent<AIMovement>();
        }

        public override TaskStatus OnUpdate()
        {
            return Animator != null 
                   && AIMovement != null 
                   && Agent != null 
                ? TaskStatus.Success 
                : TaskStatus.Failure;
        }
    }
}