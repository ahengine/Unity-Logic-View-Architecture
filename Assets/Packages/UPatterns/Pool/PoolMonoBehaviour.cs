using UnityEngine;
using System.Collections.Generic;

namespace UPatterns
{
    public class PoolMonoBehaviour<T> : MonoBehaviour where T : Component
    {
        [field:SerializeField] public Transform Parent { private set; get; }
        [field:SerializeField] public T Prefab { private set; get; }
        private List<T> items = new List<T>();

        public T[] ActiveItems
        {
            get
            {
                List<T> itemesActive = new List<T>();

                for (int i = 0; i < items.Count; i++)
                    if (items[i].gameObject.activeSelf)
                        itemesActive.Add(items[i]);

                return itemesActive.ToArray();
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

        protected virtual T CreateItem() =>
             GameObject.Instantiate(Prefab,Parent);

        private T AddNewItem()
        {
            var item = CreateItem();
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            items.Add(item);
#if UNITY_EDITOR
            item.name = name+"_"+typeof(T) + "_" + items.Count;
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
            while (items.Count > 0)
                RemoveInstance(items[0]);
        }

        public void DeactiveAllInstance()
        {
            for (int i = 0; i < items.Count; i++)
                items[i].gameObject.SetActive(false);
        } 
    }
}