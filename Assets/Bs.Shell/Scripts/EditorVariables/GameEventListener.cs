﻿using UnityEngine;
using UnityEngine.Events;

namespace Nc.Shell.Events
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;

        void OnEnable()
        {
            if (Event != null)
                Event.RegisterListener(this);
            
        }

        void OnDisable()
        {
            if(Event != null)
                Event.UnregisterListener(this);
            
        }
        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}

