using System;
using UnityEngine;
using UnityEngine.Networking;
using UHTTP;

namespace TodoModuleSystem
{
    [CreateAssetMenu(fileName = "Prodvider", menuName = "Providers/TodoProvider", order = 0)]
    public class TodoProvider : ScriptableObject
    {
        [SerializeField] private HTTPRequestCard GetAllTodos;
        public void GetAll(Action<UnityWebRequest> responseCallback) =>
            GetAllTodos.Send(responseCallback);

        [SerializeField] private HTTPRequestCard GetTodoById;
        public void GetById(int id, Action<UnityWebRequest> responseCallback)
        {
            var reqData = GetTodoById.CreateRequestData();
            reqData.SetURLAdditional(id.ToString());
            var req = reqData.CreateRequest();
            req.SetCallback(responseCallback);
            req.Send();
        }
    }
}