﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EventSystem
{
    public sealed class EventBus
    {
        private readonly Dictionary<string, List<CallbackWithPriority>> _signalCallbacks = new();

        public void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            var key = typeof(T).Name;
            if (_signalCallbacks.TryGetValue(key, out var signalCallback))
            {
                signalCallback.Add(new CallbackWithPriority(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<CallbackWithPriority> { new(priority, callback) });
            }
            
            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
        }

        public void Invoke<T>(T signal)
        {
            var key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                foreach (var obj in _signalCallbacks[key])
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            var key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                var callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
                }
            }
            else
            {
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
            }
        }
    }
}