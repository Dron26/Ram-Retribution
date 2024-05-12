using _Project_.CodeBase.Scripts.Common.Enums;
using _Project_.CodeBase.Scripts.Enemyes;
using _Project_.CodeBase.Scripts.Interfaces;
using UnityEngine;

namespace _Project_.CodeBase.Scripts.Particles
{
    public class EnemyFXHandler:IEnemyObserver
    {
        private Enemy  _enemy;
        private IEnemyObservable _observable;
        
        public void Init(Enemy  _enemy)
        {
            _enemy = _enemy;
            
            _observable = _enemy as IEnemyObservable;
            
            if (_observable != null)
                _observable.AddObserver(this);
        }
        
        public void Notifyed(EnemyEventType eventType)
        {
            switch (eventType)
            {
                case EnemyEventType.MoveStarted:
                    PlayMovementEffect();
                    break;
                case EnemyEventType.DamageReceived:
                    PlayDamageEffect();
                    break;
                case EnemyEventType.AttackStarted:
                    PlayAttackEffect();
                    break;
                case EnemyEventType.FleeStarted:
                    PlayFleeEffect();
                    break;
                default:
                    Debug.LogWarning($"Unhandled enemy event type: {eventType}");
                    break;
            }
        }
        
        public  void PlayAttackEffect()
        {
            Debug.Log("MoveStarted");
        }
        public  void PlayFleeEffect()
        {
            Debug.Log("MoveStarted");
        }
        public void PlayMovementEffect()
        {
            Debug.Log("MoveStarted");
        }

        public  void PlayDamageEffect()
        {
            Debug.Log("DamageReceived");
        }
        
        public  void OnDestroy()
        {
            if (_observable != null)
                _observable.RemoveObserver(this);
        }
    }
}