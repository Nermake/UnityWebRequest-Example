using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Extensions
{
    public static class UnityWebRequestExtension
    {
        public static async Task SendWebRequestAsync<T>(this T request) where T : UnityWebRequest
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                return;
            }

            Debug.LogError("Request failed: " + request.error);
        }
    }
}