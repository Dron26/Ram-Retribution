using _Project_.CodeBase.Scripts.Common.Enums;
using _Project_.CodeBase.Scripts.Interfaces;
using _Project_.CodeBase.Scripts.Leader;
using UnityEngine;

namespace _Project_.CodeBase.Scripts.Gate
{
    public class Gate : MonoBehaviour, IDamageable
    {
        private GateType.GateMaterial _material;
        private int _maxHealth;
        private int _currentHealth;
        private float _damageReductionModifier;
        private GateData _data;
        private Health _health;
        
        private void Init(GateData data)
        {
            _data=data;
            CalculateModifiers();
        }

        private void CalculateModifiers()
        {
            CalculateHealth();
            CalculateReduction();
        }
        
        private void CalculateHealth()
        {
            _health = new Health(_data.StrengthIndex * _data.MaxHealth,0);
        }
        private void CalculateReduction()
        {
            _damageReductionModifier = _data.ReductionModifier;
        }


        public void TakeDamage(int damage)
        {
            _currentHealth -= damage; 

            if (_currentHealth <= 0)
            {
                Die(); 
            }
        }
        private void Die()
        {
            gameObject.SetActive(false); 
        }
    }
}