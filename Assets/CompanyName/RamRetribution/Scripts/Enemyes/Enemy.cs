using System;
using _Project_.CodeBase.Scripts.Common.Enums;
using _Project_.CodeBase.Scripts.Interfaces;
using _Project_.CodeBase.Scripts.Particles;
using UnityEngine;

namespace _Project_.CodeBase.Scripts.Enemyes
{
    [RequireComponent(typeof(EnemyFXHandler))]
    public class Enemy : Component, ICharacter,IDamageable
    {
        public int health;
        public int damage;
        public float attackSpeed;
         
        public Action<EnemyEventType> OnEventNotified;

        public void NotifyEvent(EnemyEventType eventType)
        {
            OnEventNotified?.Invoke(eventType);
        }
        public void Init()
        {
            GetComponent<EnemyFXHandler>().Init(this);
        }

        public void Move(Vector3 destination)
        {
            NotifyEvent(EnemyEventType.MoveStarted);
        }

        public void Attack(ICharacter character)
        {
            NotifyEvent(EnemyEventType.AttackStarted);
        }

        public void UseAbility()
        {}

        public void TakeDamage(int amount)
        {
            NotifyEvent(EnemyEventType.DamageReceived);
        }

        public void Heal(int amount)
        {}

        public void Flee()
        {
            NotifyEvent(EnemyEventType.FleeStarted);
        }

    }
}