using System;
using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        public event Action HealthChanged;
        
        public void TakeDamage(int amount)
        {
            //_health.TakeDamage(damage);
            Debug.Log($"Получил урон: {amount}");
        }

        public void Flee()
        {
            throw new NotImplementedException();
        }

        public void Heal(int amount)
        {
            throw new NotImplementedException();
        }

        public void Move(Vector3 destination)
        {
            //_movement.Move(Vector3.zero);
            Debug.Log("Передвинулся");
        }

        public abstract void Accept(IRamVisitor visitor);
    }
}