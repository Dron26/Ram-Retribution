using CompanyName.RamRetribution.Scripts.Boot.Data;
using CompanyName.RamRetribution.Scripts.Common.Enums;
using CompanyName.RamRetribution.Scripts.Interfaces;
using CompanyName.RamRetribution.Scripts.Ram;
using TMPro;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Gate
{
    public class Gate : MonoBehaviour, IDamageable
    {
        [SerializeField] private TMP_Text _infoTextField;
        
        private GateType.GateMaterial _material;
        private int _maxHealth;
        private int _currentHealth;
        private float _damageReductionModifier;
        private Health _health;
        
        private void Init()
        {
            CalculateModifiers();
        }

        private void CalculateModifiers()
        {
            CalculateHealth();
            CalculateReduction();
        }
        
        private void CalculateHealth()
        {
        }
        
        private void CalculateReduction()
        {
            //_damageReductionModifier = _data.ReductionModifier;
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