using System.Collections;
using UnityEngine;

namespace LevelObjects.Scripts
{
    public class TileFall : MonoBehaviour
    {
        public Vector3 targetPosition;
        public float fallSpeed = 5000f;
        public float delay = 0; // Задержка перед началом падения

        void Start()
        {
            StartCoroutine(FallWithDelay());
        }

        IEnumerator FallWithDelay()
        {
            yield return new WaitForSeconds(delay);

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}