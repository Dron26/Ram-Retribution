using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public abstract class Unit : MonoBehaviour
    {
        private readonly Health _health;
        private readonly Movement _movement;
        private readonly int _damage;
        
        public void TakeDamage(int damage)
        {
            //_health.TakeDamage(damage);
            Debug.Log($"Получил урон: {damage}");
        }

        public void Move(Vector3 destination)
        {
            //_movement.Move(Vector3.zero);
            Debug.Log("Передвинулся");
        }

        public void ShowInfo()
        {
            Debug.Log($"Health {_health.Value}, Movement {_movement != null}, Armor {_health.Armor}, Damage {_damage}");
        }

        public abstract void Accept(IRamVisitor visitor);
    }
}