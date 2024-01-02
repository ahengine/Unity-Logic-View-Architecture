using System.Collections;
using UHTTP;
using UnityEngine;
using UnityEngine.Networking;

namespace TodoModuleSystem
{
    public class TodoSampleRunner : MonoBehaviour
    {
        [SerializeField] private int Id;
        [SerializeField] private string GetURLSample = "https://jsonplaceholder.typicode.com/todos/11";

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
                TodoController.Instance.ShowAll();

            if(Input.GetKeyDown(KeyCode.Alpha2))
                TodoController.Instance.ShowById(Id);
            
            if(Input.GetKeyDown(KeyCode.Alpha0))
                Get();
        }

        private void Get() =>
            StartCoroutine(GetCoroutine());
        private IEnumerator GetCoroutine() 
        {
            UnityWebRequest req = new UnityWebRequest()
                    {
                        method = HTTPRequestMethod.GET.ToString(),
                        url = GetURLSample
                    };

            
            
            yield return req.SendWebRequest();
            Debug.Log(req.downloadHandler.text);
        }
    }
}
