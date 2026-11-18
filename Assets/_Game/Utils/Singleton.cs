// Author: Murillo Gomes Yonamine
// Date: 08/11/2026

using UnityEngine;

namespace GOAP.GAME
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance;

        [SerializeField] private bool _shouldDestroyOnLoad = true;

        public static T EnsureInstance()
        {
            if (_instance != null) return _instance;

            T existing = FindFirstObjectByType<T>();
            if (existing != null) return existing;

            GameObject managerObject = new GameObject(typeof(T).Name);
            return managerObject.AddComponent<T>();
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            if (!_shouldDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            _instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
    }
}