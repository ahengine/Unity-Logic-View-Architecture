using System.Collections.Generic;

namespace UHTTP
{
    public struct HTTPRequestData
    {
        // URL
        public string URLFull => URL + URLAdditional;
        public string URL { private set; get; }
        public void SetURL(string URL) =>
            this.URL = URL; 

        public string URLAdditional { private set; get; } 
        public void SetURLAdditional(string URLAdditional) =>
            this.URLAdditional = URLAdditional;

        // METHOD
        public string Method { private set; get; }
        public void SetMethod(string method) =>
            Method = method;

        // AUTH
        public bool HaveAuth { private set; get; }
        public void SetAuth(bool haveAuth) =>
            HaveAuth = haveAuth;  

        // HEADER
        public List<KeyValuePair<string, string>> Headers { private set; get; }
        public void AddHeader( KeyValuePair<string, string> newHeader) =>
            Headers.Add(newHeader);
        public void ClearHeaders() =>
            Headers.Clear();


        // --------------POST--------------

        // BODY JSON
        public string BodyJson { private set; get; }
        public void SetBodyJson(string bodyJson) =>
            BodyJson = bodyJson;

        // POST FIELD
        public Dictionary<string, string> PostFields { private set; get; }
        public void AddPostField(string key, string value) =>
            PostFields.Add(key,value);
        public void ClearPostFields() =>
            PostFields.Clear();

        // POST FORM FIELD
        public Dictionary<string, string> PostFormFields { private set; get; }
        public void AddPostFormField(string key, string value) =>
            PostFormFields.Add(key,value);
        public void ClearPostFormFields() =>
            PostFormFields.Clear();

        public HTTPRequest CreateRequest() =>
            new HTTPRequest(this);
    }
}