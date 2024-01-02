using System;
using System.Collections.Generic;
using UnityEngine;

namespace UScreens
{
    public static class UScreenRepo
    {
        private static Dictionary<Type, UScreen> Screens = new Dictionary<Type, UScreen>();

        public static T Get<T>() where T : UScreen
        {
            if (!Screens.ContainsKey(typeof(T)))
                Create<T>();

            return Screens[typeof(T)] as T;
        }
        public static void Remove<T>() where T : UScreen =>
            Screens.Remove(typeof(T));

        public static void Create<T>() where T : UScreen
        {
            var instance = new GameObject(typeof(T).Name).AddComponent<T>();
            instance.InitializeState();
            Add(instance);
        }
        
        private static void Add<T>(T screen) where T : UScreen =>
            Screens.Add(typeof(T), screen);
    }
}