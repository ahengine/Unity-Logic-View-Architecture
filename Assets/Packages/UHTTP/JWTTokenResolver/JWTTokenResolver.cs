using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UHTTP
{
    public static class JWTTokenResolver
    {
        public const string AUTHORIZATION_HEADER_KEY = "Authorization";
        private const string ACCESS_TOKEN_KEY = "JWT_ACCESS_TOKEN";
        private const string REFRESH_TOKEN_KEY = "JWT_REFRESH_TOKEN";

        private static HTTPRequestCard RefreshTokenCard;
        private static Action<string> AccessTokenResolverFromRefreshResponse;
        public static void SetRefreshTokenData(HTTPRequestCard refreshTokenCard, Action<string> SetNewAccessToken) 
        {
            RefreshTokenCard = refreshTokenCard;
            AccessTokenResolverFromRefreshResponse = SetNewAccessToken;
        }

        public static string AccessToken =>
            PlayerPrefs.GetString(ACCESS_TOKEN_KEY, "");
        public static string RefreshToken =>
            PlayerPrefs.GetString(REFRESH_TOKEN_KEY, "");

        public static KeyValuePair<string, string> AccessTokenHeader =>
            new KeyValuePair<string, string>(AUTHORIZATION_HEADER_KEY,"Bearer "+ AccessToken);
        public static KeyValuePair<string, string> RefreshTokenHeader =>
            new KeyValuePair<string, string>(AUTHORIZATION_HEADER_KEY, RefreshToken);

        public static void SetAccessToken(string token) =>
            PlayerPrefs.SetString(ACCESS_TOKEN_KEY, token);
        public static void SetRefreshToken(string token) =>
            PlayerPrefs.SetString(REFRESH_TOKEN_KEY, token);

        public static void RemoveTokens()
        {
            PlayerPrefs.DeleteKey(ACCESS_TOKEN_KEY);
            PlayerPrefs.DeleteKey(REFRESH_TOKEN_KEY);
        }
        public static void RemoveAccessToken() =>
            PlayerPrefs.DeleteKey(ACCESS_TOKEN_KEY);

        public static void ResolveAccessToken(Action requestAction)
        {
            if (string.IsNullOrEmpty(RefreshToken))
            {
                requestAction?.Invoke();
                return;
            }

            void Resolve(UnityWebRequest request)
            {
                if (request.responseCode == (int)HTTPResponseCodes.UNAUTHORIZED_401 ||
                    request.responseCode == (int)HTTPResponseCodes.FORBIDEN_403)
                    RemoveTokens();
                else
                    AccessTokenResolverFromRefreshResponse?.Invoke(request.downloadHandler.text);

                requestAction?.Invoke();
            }

            var req = new HTTPRequest()
            {
                callback = Resolve
            };
            req.SetCard(RefreshTokenCard.CreateRequestData());
            req.Send();
        }
    }   
}