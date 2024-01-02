using System.Collections;
using UnityEngine;

namespace UHTTP
{
    public class HTTPRequestCoroutineRunner : MonoBehaviour
    {
        private static HTTPRequestCoroutineRunner instance;
        private static HTTPRequestCoroutineRunner Instance => instance ?? (instance = new GameObject(typeof(HTTPRequestCoroutineRunner).Name).AddComponent<HTTPRequestCoroutineRunner>());

        private void OnDestroy() =>
            instance = null;

        public static Coroutine Run(IEnumerator enumerator) =>
            Instance.StartCoroutine(enumerator);
    }
}