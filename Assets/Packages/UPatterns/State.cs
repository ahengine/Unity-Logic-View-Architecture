using UnityEngine;

namespace UPatterns
{
    public abstract class State<T> : MonoBehaviour
    {
        [field: SerializeField] public T ID { protected set; get; }

        virtual public void Enter() { }
        virtual public void Exit() { }
        virtual public void Updates() { }
        virtual public void LateUpdates() { }
        virtual public void FixedUpdates() { }
    }
}