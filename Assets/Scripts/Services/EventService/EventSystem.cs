using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyService
{
    public class MyEventSystem : Singleton<MyEventSystem>
    {
        private Dictionary<int, EventHandler<GameEventArgs>> m_EventHandle  = new Dictionary<int, EventHandler<GameEventArgs>>();

        public void Subscribe(int id, EventHandler<GameEventArgs> handler)
        {
            if (m_EventHandle.ContainsKey(id))
            {
                m_EventHandle[id] += handler;
            }
            else
            {
                m_EventHandle.Add(id, handler);
            }

        }

        public void UnSubscribe(int id, EventHandler<GameEventArgs> handler)
        {
            if (m_EventHandle.ContainsKey(id) && m_EventHandle[id] != null)
            {
                m_EventHandle[id] -= handler;
            }

        }

        public void Invoke(int id, object sender, GameEventArgs args)
        {
            if (m_EventHandle.ContainsKey(id) && m_EventHandle[id] != null)
            {
                m_EventHandle[id](sender, args);
            }
        }
    }
}
