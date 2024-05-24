using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public abstract class Unit : MonoBehaviour
    {
        public abstract void TakeDamage(int amount);
        public abstract void Heal(int amount);
        public abstract void Move(Vector3 target);
    }
}