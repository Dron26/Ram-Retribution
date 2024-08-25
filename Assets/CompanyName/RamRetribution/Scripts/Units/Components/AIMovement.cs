using System;
using System.Collections;
using System.Threading;
using CompanyName.RamRetribution.Scripts.Common;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace CompanyName.RamRetribution.Scripts.Units.Components
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIMovement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.enabled = false;
            enabled = false;
        }

        public void Init(Animator animator) 
            => _animator = animator;

        public async UniTask<bool> MoveToPoint(Vector3 destination, CancellationToken cancellationToken, Action callback = null)
        {
            _animator.SetBool(AIAnimatorParams.Run, true);

            while ((destination - transform.position).sqrMagnitude > _agent.stoppingDistance + float.Epsilon)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, destination, _agent.speed * Time.deltaTime);
                await UniTask.Yield();
            }

            _animator.SetBool(AIAnimatorParams.Run, false);
            transform.position = destination;
            callback?.Invoke();

            return !cancellationToken.IsCancellationRequested;
        }

        public async UniTask MoveTowards(Transform target, CancellationToken cancellationToken)
        {
            _agent.ResetPath();
            enabled = true;
            _animator.SetBool(AIAnimatorParams.Run, true);

            _agent.SetDestination(target.position);
            
            while ((target.position - _agent.transform.position).sqrMagnitude > _agent.stoppingDistance + float.Epsilon)
            {
                _agent.SetDestination(target.position);

                await UniTask.Delay(
                    TimeSpan.FromSeconds(0.5f),
                    DelayType.Realtime, 
                    cancellationToken: cancellationToken);
            }

            _animator.SetBool(AIAnimatorParams.Run, false);
            enabled = false;
        }

        public void ActivateNavMesh() 
            => _agent.enabled = true;

        public void DeactivateNavMesh() 
            => _agent.enabled = false;
    }
}