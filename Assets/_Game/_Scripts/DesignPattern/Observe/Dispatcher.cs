using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public abstract class Dispatcher<T> : Singleton<T> where T : MonoBehaviour
    {
        private readonly Dictionary<EventID, Action> _listenerEventDictionary = new();
        private readonly Dictionary<EventID, Action<object>> _listenerEventDictionaryParam = new();
        public void RegisterListenerEvent(EventID eventID, Action callback)
        {
            if (_listenerEventDictionary.ContainsKey(eventID))
                _listenerEventDictionary[eventID] += callback;
            else
                _listenerEventDictionary.Add(eventID, callback);
        }

        public void RegisterListenerEvent(EventID eventID, Action<object> callback)
        {
            if (_listenerEventDictionaryParam.ContainsKey(eventID))
                _listenerEventDictionaryParam[eventID] += callback;
            else
                _listenerEventDictionaryParam.Add(eventID, callback);
        }

        public void UnregisterListenerEvent(EventID eventID, Action callback)
        {
            if (_listenerEventDictionary.ContainsKey(eventID))
                _listenerEventDictionary[eventID] -= callback;
            else
                Debug.LogWarning("EventID " + eventID + " not found");
        }

        public void UnregisterListenerEvent(EventID eventID, Action<object> callback)
        {
            if (_listenerEventDictionaryParam.ContainsKey(eventID))
                _listenerEventDictionaryParam[eventID] -= callback;
            else
                Debug.LogWarning("EventID " + eventID + " not found");
        }

        public void PostEvent(EventID eventID)
        {
            if (_listenerEventDictionary.TryGetValue(eventID, out Action value))
                value?.Invoke();
            else
                Debug.LogWarning("EventID " + eventID + " not found");
        }
        
        public void PostEvent(EventID eventID, object param)
        {
            if (_listenerEventDictionaryParam.TryGetValue(eventID, out Action<object> value))
                value.Invoke(param);
            else
                Debug.LogWarning("EventID " + eventID + " not found");
        }

        public void ClearAllListenerEvent()
        {
            _listenerEventDictionary.Clear();
        }
    }

    public enum EventID
    {
        //NOTE: Gameplay
        AddSlot = 0,
        Mix = 1,
        Exchange = 2,

        //NOTE: System
        OnDespawnLevel = 1000,
        LevelEnd = 1001,
    }
}
