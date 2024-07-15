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
        private Action _completeAction;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.enabled = false;
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

        public void Init(Animator animator)
        {
            _animator = animator;
        }
        
        public void Move(Vector3 destination, Action callback = null)
        {
            StartCoroutine(MoveToPoint(destination, callback));
        }

        public async UniTask MoveTowards(Transform target, CancellationToken cancellationToken)
        {
            _agent.ResetPath();
            enabled = true;
            
            while (enabled)
            {
                _agent.SetDestination(target.position);

                await UniTask.Delay(
                        TimeSpan.FromSeconds(0.5f),
                        DelayType.DeltaTime
                    )
                    .WithCancellation(cancellationToken);
            }
        }

        public void OnComplete(Action callback)
        {
            _completeAction = callback;
        }

        public void ActivateNavMesh()
        {
            _agent.enabled = true;
        }

        public void DeactivateNavMesh()
        {
            _agent.enabled = false;
        }

        private IEnumerator MoveToPoint(Vector3 destination, Action callback = null)
        {
            _animator.SetBool(AIAnimatorParams.Run, true);

            while ((destination - transform.position).sqrMagnitude > _agent.stoppingDistance + float.Epsilon)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, destination, _agent.speed * Time.deltaTime);
                yield return null;
            }

            _animator.SetBool(AIAnimatorParams.Run, false);
            transform.position = destination;
            callback?.Invoke();
        }
    }
}