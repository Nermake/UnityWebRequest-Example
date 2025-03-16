using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using UnityEngine;
using UnityEngine.Networking;

namespace Logic.Core
{
    public class RequestManager
    {
        private readonly Queue<RequestData> requestQueue = new();
        private UnityWebRequest currentRequest;
        
        private struct RequestData
        {
            public UnityWebRequest Request;
            public Action<string> Callback;

            public RequestData(UnityWebRequest request, Action<string> callback)
            {
                Request = request;
                Callback = callback;
            }
        }
        
        public void AddRequest(UnityWebRequest request, Action<string> callback)
        {
            var requestData = new RequestData(request, callback);
            requestQueue.Enqueue(requestData);
            ProcessNextRequest();
        }

        public void CancelLastRequest()
        {
            if (requestQueue.Count > 0)
            {
                var lastRequestData = requestQueue.Dequeue();
                lastRequestData.Request.Abort();
                Debug.Log("Last request canceled.");
            }
        }

        private async void ProcessNextRequest()
        {
            if (currentRequest != null || requestQueue.Count <= 0) return;
            
            var requestData = requestQueue.Dequeue();
            currentRequest = requestData.Request;
            await ExecuteRequest(requestData);
        }

        private async Task ExecuteRequest(RequestData requestData)
        {
            var request = requestData.Request;
            await request.SendWebRequestAsync();

            string result = null;

            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                result = request.downloadHandler.text;
            }

            requestData.Callback?.Invoke(result);

            currentRequest = null;
            ProcessNextRequest();
        }
    }
}