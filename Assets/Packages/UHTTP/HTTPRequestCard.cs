using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UHTTP
{
        [CreateAssetMenu(fileName = "HTTPRequestCard", menuName = "Cards/HTTPRequestCard", order = 0)]
        public partial class HTTPRequestCard : ScriptableObject
        {
                [SerializeField] private List<KeyValueItem> headers;
                [field: SerializeField] public string URL { private set; get; }
                [field: SerializeField] public HTTPRequestMethod Method { private set; get; }
                [field: SerializeField] public bool HaveAuth { private set; get; }

                // POST
                [field: SerializeField] public string BodyJson { private set; get; }
                [SerializeField] private List<KeyValueItem> postFields;
                [SerializeField] private List<KeyValueItem> postFormFields;

                public HTTPRequestData CreateRequestData()
                {
                        var data = new HTTPRequestData();
                        data.SetURL(URL);
                        string GetMethod()
                        {
                                switch (Method)
                                {
                                        case HTTPRequestMethod.GET:
                                        default:
                                                return UnityWebRequest.kHttpVerbGET;
                                        case HTTPRequestMethod.POST:
                                                return UnityWebRequest.kHttpVerbPOST;
                                        case HTTPRequestMethod.PUT:
                                                return UnityWebRequest.kHttpVerbPUT;
                                }
                        }
                        data.SetMethod(GetMethod());
                        data.SetAuth(HaveAuth);

                        // POST
                        data.SetBodyJson(BodyJson);
                        for (int i = 0; i < headers.Count; i++)
                                data.AddHeader(new KeyValuePair<string, string>(headers[i].key, headers[i].value));
                        for (int i = 0; i < postFields.Count; i++)
                                data.AddPostField(postFields[i].key, postFields[i].value);
                        for (int i = 0; i < postFields.Count; i++)
                                data.AddPostFormField(postFields[i].key, postFields[i].value);

                        return data;
                }

                public HTTPRequest CreateRequest() =>
                        CreateRequestData().CreateRequest();

                public HTTPRequest Send(Action<UnityWebRequest> callback) =>
                        CreateRequest().SetCallback(callback).Send();
        }

        [Serializable]
        public class KeyValueItem
        {
                public string key;
                public string value;
        }
}