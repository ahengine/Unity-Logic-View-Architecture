using System;

namespace UPatterns
{
    public class ObserverVariable<T>
    {
        private T m_Value;
        public T Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                OnChanged?.Invoke(value);
            }
        }

        public event Action<T> OnChanged;

        public void SetEvent(Action<T> action) =>
            this.OnChanged = action;
    }
}