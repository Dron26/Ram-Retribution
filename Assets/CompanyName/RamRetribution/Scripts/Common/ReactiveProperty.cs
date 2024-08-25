using System;

namespace CompanyName.RamRetribution.Scripts.SkillsModule.Infrastructure
{
    public class ReactiveProperty<T>
    {
        private T _value;

        public event Action<T> OnValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}