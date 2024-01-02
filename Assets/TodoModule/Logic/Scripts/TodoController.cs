using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

namespace TodoModuleSystem
{
    public class TodoController : MonoBehaviour
    {
        private static TodoController instance;
        public static TodoController Instance => instance ?? CreateInstance();
        private TodoProvider provider;

        private static TodoController CreateInstance()
        {
            instance = new GameObject(typeof(TodoController).Name).AddComponent<TodoController>();
            instance.provider = Resources.Load<TodoProvider>(typeof(TodoProvider).Name);
            return instance;
        }

        public void ShowAll(Action<Todo[]> GetItems = null)
        {
            provider.GetAll(Callback);
            void Callback(UnityWebRequest response)
            {
                switch (response.result)
                {
                    case UnityWebRequest.Result.Success:
                            ApplyShowAll(response.downloadHandler.text,GetItems);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        print("Get All Todos: Failed to fetch \n" + response.error);
                        break;
                }
            }


            void ApplyShowAll(string json, Action<Todo[]> GetItems = null)
            {
                var todos = JsonConvert.DeserializeObject<List<Todo>>(json);
                GetItems?.Invoke(todos.ToArray());
                for (int i = 0; i < todos.Count; i++)
                    Debug.Log(todos[i].ToString());
            }
        }


        public void ShowById(int id, Action<Todo[]> GetItems = null)
        {
            provider.GetById(id, Callback);
            void Callback(UnityWebRequest response)
            {
                switch (response.result)
                {
                    case UnityWebRequest.Result.Success:
                        ApplyShowById(response.downloadHandler.text, GetItems);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        print("Get Todo by id " + id + " : Failed to fetch \n" + response.error);
                        break;
                }
            }

            void ApplyShowById(string json, Action<Todo[]> GetItems)
            {
                var todo = JsonConvert.DeserializeObject<Todo>(json);
                GetItems?.Invoke(new Todo[] {todo });
                Debug.Log(todo.ToString());
            }
        }
    }
}