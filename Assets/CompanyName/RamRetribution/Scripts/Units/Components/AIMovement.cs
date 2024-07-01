using System;
using System.Collections;
using CompanyName.RamRetribution.Scripts.Common;
using DG.Tweening;
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
        private Coroutine _moveTowardsCoroutine;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();

            //_agent.enabled = false;
            enabled = false;
        }

        private void Update()
        {
            _animator.SetBool(AIAnimatorParams.Run, true);

            if (_agent.remainingDistance < _agent.stoppingDistance + float.Epsilon)
            {
                _completeAction?.Invoke();
                _completeAction = null;

                _animator.SetBool(AIAnimatorParams.Run, false);
                enabled = false;
            }
        }

        public void Move(Vector3 destination, Action callback = null)
        {
            StartCoroutine(MoveToPoint(destination, callback));
        }

        public AIMovement MoveTowards(Transform target)
        {
            if (_moveTowardsCoroutine != null)
                StopCoroutine(_moveTowardsCoroutine);

            _agent.ResetPath();
            enabled = true;
            _moveTowardsCoroutine = StartCoroutine(MoveTowardsMovingTarget(target));

            return this;
        }

        public void OnComplete(Action callback)
        {
            _completeAction = callback;
        }

        public void ActivateNavMesh()
        {
            _agent.enabled = true;
        }

        private IEnumerator MoveTowardsMovingTarget(Transform target)
        {
            while (enabled)
            {
                if (!target.gameObject.activeSelf && target == null)
                {
                    yield break;
                }

                var destination = target.position;
                _agent.SetDestination(destination);

                yield return null;
            }
        }

        private IEnumerator MoveToPoint(Vector3 destination, Action callback = null)
        {
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position, destination, _agent.speed * Time.deltaTime);
                yield return null;
            }

            transform.position = destination;
            callback?.Invoke();
        }
    }
}