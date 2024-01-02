using UnityEngine;
using System;

namespace UPatterns
{
    public abstract class Module<TOwner, TBaseModules> : MonoBehaviour where TOwner : MonoBehaviour where TBaseModules : Module<TOwner, TBaseModules>
    {
        protected TOwner owner;
        public virtual bool AllowHorizontalMove => false;

        public virtual bool IsActive { protected set; get; }
        protected float startTime;

        [SerializeField] protected TBaseModules[] activateDependencies;

        public event Action OnComplete;

        public bool AllowActivate()
        {
            if (!AllowActivateModule) return false;

            for (int i = 0; i < activateDependencies.Length; i++)
                if (activateDependencies[i].IsActive) return false;

            return true;
        }
        protected virtual bool AllowActivateModule { set; get; } = true;

        public virtual void SetOwner(TOwner owner)
            => this.owner = owner;

        public void DoActivate()
        {
            if (!AllowActivate()) return;

            IsActive = true;
            startTime = Time.time;

            ApplyActivate();
        }

        protected abstract void ApplyActivate();

        public virtual void Process() { }

        public void DoDeactivate()
        {
            if (!IsActive) return;

            OnComplete.Invoke();
            IsActive = false;
            ApplyDeactivate();
        }

        protected abstract void ApplyDeactivate();
    }
}