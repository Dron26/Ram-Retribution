using System;

namespace CompanyName.RamRetribution.Scripts.Common
{
    public class CooldownTimer
    {
        private float _initialTime;

        public CooldownTimer(float initialTime)
        {
            _initialTime = initialTime;
            IsRunning = false;
        }
        
        public event Action OnTimerStart;
        public event Action OnTimerStop;
        
        public float Time { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsFinished => Time <= 0;
        
        public void Start()
        {
            Time = _initialTime;

            if (IsRunning) return;
            
            IsRunning = true;
            OnTimerStart?.Invoke();
        }

        public void Stop()
        {
            if (!IsRunning) return;
            
            IsRunning = false;
            OnTimerStop?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if (IsRunning && Time > 0)
                Time -= deltaTime;

            if (IsRunning && IsFinished)
                Stop();
        }
        
        public void Reset() 
            => Time = _initialTime;

        public void Reset(float newTime)
        { 
            _initialTime = newTime;
            Reset();
        } 
    }
}