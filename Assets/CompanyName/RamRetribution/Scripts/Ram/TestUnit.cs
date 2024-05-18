using CompanyName.RamRetribution.Scripts.Interfaces;
using TMPro;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts.Ram
{
    public class TestUnit : MonoBehaviour, IBind<LeaderDataState>
    {
        [SerializeField] private TMP_Text _infoTextField;
        
        private int _health;
        private int _armor;
        private int _damage;
        private float _attackSpeed;
        
        private LeaderDataState _leaderDataState;
        
        public void Bind(LeaderDataState dataState)
        {
            _leaderDataState = dataState;
            _health = _leaderDataState.Health;
            _armor = _leaderDataState.Armor;
            _damage = _leaderDataState.Damage;
            _attackSpeed = _leaderDataState.AttackSpeed;
            
            ShowInfo();
        }

        private void ShowInfo()
        {
            _infoTextField.text = string.Format($"Health: {_health} " +
                                                $"\nArmor: {_armor}" +
                                                $"\nDamage: {_damage}" +
                                                $"\nASpeed: {_attackSpeed} ");
        }
    }
}