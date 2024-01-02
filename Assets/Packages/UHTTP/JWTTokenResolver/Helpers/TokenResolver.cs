using UnityEngine;

namespace UHTTP.Helpers
{
    [CreateAssetMenu(fileName ="Token Resolver",menuName ="Cards/Token Resolver",order = 0)]
    public class TokenResolver : ScriptableObject
    {
        [SerializeField, TextArea] private string token;

        [ContextMenu("Resolve Token")]
        private void ResolveToken()
        {
            JWTTokenResolver.SetAccessToken(token);
            Debug.Log("Access Token Updated \n" + token);
        }

        [ContextMenu("Resolve Refresh Token")]
        private void ResolveRefreshToken()
        { 
            JWTTokenResolver.SetRefreshToken(token); 
            Debug.Log("Refresh Token Updated \n" + token);
        }
    }
}