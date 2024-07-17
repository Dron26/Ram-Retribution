using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Common
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }

        public static Vector3 RandomPointInCircle(this Vector3 origin, float minRadius, float maxRadius)
        {
            float angle = Random.value * Mathf.PI * 2f;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            float minRadiusSqr = minRadius * minRadius;
            float maxRadiusSqr = maxRadius * maxRadius;
            float distance = Mathf.Sqrt(Random.value * (maxRadiusSqr - minRadiusSqr) + minRadiusSqr);
            Vector3 position = Vector3.zero.With(x: direction.x, z: direction.y) * distance;
            return origin + position;
        }
    }
}