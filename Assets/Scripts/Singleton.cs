using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyService
{
    public class Singleton<T>  where T : class , new()
    {
        private static object LockObject = new object();
        private static T m_Instance = null;

        public static T Instance {
            get
            {
                if(m_Instance == null)
                {
                    lock(LockObject)
                    {
                        m_Instance = new T();
                    }

                }
                return m_Instance;
            }
        }
    }
}
