using CompanyName.RamRetribution.Scripts.Interfaces;
using UnityEngine;

namespace CompanyName.RamRetribution.Scripts
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        public int health;
        public int damage;
        public float attackSpeed;

        public void Move(Vector3 destination)
        {}

        public void Attack(ICharacter character)
        {}

        public void UseAbility()
        {}

        public void TakeDamage(int amount)
        {}

        public void Heal(int amount)
        {}

        public void Flee()
        {}
    }
}