using System;
using System.Collections.Generic;
using UnityEngine;

namespace UPatterns
{
    [Serializable]
    public class Pool<T> where T : Component
    {
        [field:SerializeField] public Transform Parent { private set; get; }
        [field:SerializeField] public T Prefab { private set; get; }
        private List<T> items = new List<T>();
        private Func<Transform,T> factory;
        
        public void SetFactory(Func<Transform,T> factory) =>
            this.factory = factory;

        /// <summary>
        /// Expensive Property
        /// </summary>
        public T[] ActiveItems
        {
            get
            {
                List<T> _itemesActive = new List<T>();

                for (int i = 0; i < items.Count; i++)
                    if (items[i].gameObject.activeSelf)
                        _itemesActive.Add(items[i]);

                return _itemesActive.ToArray();
            }
        }

        public T Get
        {
            get
            {
                for (int i = 0; i < items.Count; i++)
                    if (!items[i].gameObject.activeSelf)
                        return items[i];

                return AddNewItem();
            }
        }

        public T GetActive { get { var t = Get; t.gameObject.SetActive(true); return t; } }

        private T AddNewItem()
        {
            var item = factory != null ? factory(Parent) : GameObject.Instantiate(Prefab, Parent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            items.Add(item);
#if UNITY_EDITOR
            item.name = typeof(T) + "_" + items.Count;
#endif
            return item;
        }
        
        public void RemoveInstance(T item)
        {
            items.Remove(item);
            GameObject.Destroy(item);
        }

        public void RemoveAllInstance()
        {
            while(items.Count > 0)
                RemoveInstance(items[0]);
        }  

        public void DeactiveAllInstance()
        {
            for (int i = 0; i < items.Count; i++)
                items[i].gameObject.SetActive(false);
        } 
    }
}