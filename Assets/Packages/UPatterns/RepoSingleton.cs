using System.Collections.Generic;
using UnityEngine;

namespace UPatterns
{
    public static class RepoSingleton
    {
        private static List<Component> instances = new List<Component>();
    
        public static T GetInstance<T>() where T : Component
        {
            for (int i = 0; i < instances.Count; i++)
                if (instances[i] is T)
                    return instances[i] as T;
    
            return null;
        }
        public static void SetInstance<T>(T instance) where T : Component
        {
            for (int i = 0; i < instances.Count; i++)
                if (instances[i] is T)
                {
                    instances[i] = instance;
                    return;
                }
    
            instances.Add(instance);
        }
        public static void RemoveInstance<T>(bool allowDestroyInstance = true) where T : Component
        {
            for (int i = 0; i < instances.Count; i++)
                if (instances[i] is T)
                {
                    if (allowDestroyInstance && instances[i])
                        GameObject.Destroy(instances[i]);
                    instances.RemoveAt(i);
                    return;
                }
        }
        
        public static void ClearAllInstance(bool allowDestroyInstance = true)
        {
            if(allowDestroyInstance)
                for (int i = 0; i < instances.Count; i++)
                    if(instances[i])
                        GameObject.Destroy(instances[i]);
    
            instances.Clear();
        }
    }
}
