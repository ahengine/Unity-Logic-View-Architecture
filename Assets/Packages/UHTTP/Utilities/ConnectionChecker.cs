using UnityEngine;
using IEnumerator = System.Collections.IEnumerator;

namespace UHTTP.Helpers
{
    public class ConnectionChecker : MonoBehaviour
    {
        private static ConnectionChecker instance;

        private void OnDestroy() {
            instance = null;
        }

        public static int CheckingTime { private set; get; } = 5;
        public static void SetCheckingTime(int value) =>
            CheckingTime = value;

        public static bool AllowCheckingConnection { private set; get; } = true;
        public static void SetAllowCheckingConnection(bool value) =>
            AllowCheckingConnection = value;


        public static void StartChecking()
        {
            if (!instance)
                instance = new GameObject(typeof(ConnectionChecker).Name).AddComponent<ConnectionChecker>();

            instance.StopAllCoroutines();
            instance.StartCoroutine(Checking());
        }

        public static bool IsConnected { private set; get; }
        private static IEnumerator Checking()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(CheckingTime);
                if (AllowCheckingConnection)
                {
                    Ping request = new Ping("http://google.com");
                    while (!request.isDone)
                        yield return new WaitForEndOfFrame();

                    //IsConnected = request.time
                }
            }
        }
    }
}

