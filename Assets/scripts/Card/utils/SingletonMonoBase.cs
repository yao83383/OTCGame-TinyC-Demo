using UnityEngine;

#if UNITY_EDITOR
using System;
#endif

namespace AStar.Utils.DesignPattern.Singleton
{
    public abstract class SingletonMonoBase<T> : MonoBehaviour where T : SingletonMonoBase<T>
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
					m_Instance = FindObjectOfType<T>();
					
					if(m_Instance == null)
					{
						GameObject managerObject = new GameObject(typeof(T).Name);
						m_Instance = managerObject.AddComponent<T>();
					}
                }
                return m_Instance;
            }
        }

        public virtual void Awake()
        {
            T self = this as T;
            if (m_Instance == null)
            {
                m_Instance = self;
                DontDestroyOnLoad(m_Instance);
            }
            else if (m_Instance != self)
            {
                DestroyImmediate(m_Instance);
                m_Instance = self;
                DontDestroyOnLoad(m_Instance);
            }
        }
    }
}