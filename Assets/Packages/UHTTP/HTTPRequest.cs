using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using JWTResolver = UHTTP.JWTTokenResolver;
using static UHTTP.HTTPRequestCoroutineRunner;
using System.Collections;

namespace UHTTP
{
    public class HTTPRequest
    {
        public HTTPRequestData Data { private set; get; }
        public Action<UnityWebRequest> callback;

        public HTTPRequest() { }
        public HTTPRequest(HTTPRequestData data) =>
            Data = data;

        public HTTPRequest SetCallback(Action<UnityWebRequest> callback) 
        {
            this.callback = callback;
            return this;
        }
           
        public void SetCard(HTTPRequestData data) =>
            Data = data;

        private UnityWebRequest CreateRequest()
        {
            UnityWebRequest request = new UnityWebRequest()
                    {
                        method = Data.Method,
                        url = Data.URLFull
                    };
            
            void AddBody()
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(Data.BodyJson);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }

            // Add Body
            if (!string.IsNullOrEmpty(Data.BodyJson))
                AddBody();

            void SetHeaders()
            {
                List<KeyValuePair<string, string>> totalHeaders = new List<KeyValuePair<string, string>>();

                if(Data.Headers != null)
                    totalHeaders.AddRange(Data.Headers);

                // Add Defaults
                totalHeaders.AddRange(new KeyValuePair<string, string>[]  {
                    new KeyValuePair<string, string>("Content-Type", "application/json"),
                    new KeyValuePair<string, string>("Accept", "application/json")
                });

                // Add JWT
                if (Data.HaveAuth && !string.IsNullOrEmpty(JWTResolver.AccessToken))
                    totalHeaders.Add(JWTResolver.AccessTokenHeader);
                
                // Set
                if (totalHeaders != null && totalHeaders.Count > 0)
                    foreach (var header in totalHeaders)
                        request.SetRequestHeader(header.Key, header.Value);
            }

            SetHeaders();
            return request;
        }   

        public HTTPRequest Send() 
        {
            Run(SendCoroutine());
            return this;
        }

        public IEnumerator SendCoroutine()
        {
            void ReviewToken(UnityWebRequest request)
            {
                if (request.responseCode != (int)HTTPResponseCodes.UNAUTHORIZED_401 && 
                    request.responseCode != (int)HTTPResponseCodes.FORBIDEN_403)
                {
                    callback(request);
                    return;
                }

                JWTResolver.RemoveAccessToken();

                if (Data.HaveAuth)
                {
                    JWTResolver.ResolveAccessToken(() => Send());
                    return;
                }
 
                callback(request);
            }

            var request = CreateRequest();
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (Data.HaveAuth)
                ReviewToken(request);
            else 
                callback?.Invoke(request);
            request.Dispose();
        }
    }
}