using UnityEngine;

namespace SubScript.Singleton
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static readonly object lockObject = new object();
        private static bool isQuitting = false;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit.");
                    return null;
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        // Tìm instance trong scene
                        instance = FindObjectOfType<T>();

                        // Kiểm tra nhiều instance
                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            Debug.LogError($"[Singleton] Multiple instances of {typeof(T)} found!");
                            return instance;
                        }

                        // Tạo mới nếu chưa có
                        if (instance == null)
                        {
                            var go = new GameObject($"[Singleton] {typeof(T)}");
                            instance = go.AddComponent<T>();

                            // Cho phép override trong derived class
                            (instance as SingletonBase<T>)?.OnSingletonCreated();
                        }
                    }
                    return instance;
                }
            }
        }

        protected virtual void OnSingletonCreated()
        {
            // Override trong derived class nếu cần
        }

        protected virtual void OnApplicationQuit()
        {
            isQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            isQuitting = true;
        }
    }


    public abstract class SingletonThroughScene<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        private static readonly object lockObject = new object();
        private static bool isQuitting = false;
        private static bool initialized = false;

        public static T Instance
        {
            get
            {
                if (isQuitting)
                {
                    Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit.");
                    return null;
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        // Tìm instance trong scene
                        instance = FindObjectOfType<T>();

                        // Kiểm tra nhiều instance
                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            Debug.LogError($"[Singleton] Multiple instances of {typeof(T)} found!");
                            return instance;
                        }

                        // Tạo mới nếu chưa có
                        if (instance == null)
                        {
                            var go = new GameObject($"[Singleton] {typeof(T)}");
                            instance = go.AddComponent<T>();

                            // Cho phép override trong derived class
                            (instance as SingletonThroughScene<T>)?.OnSingletonCreated();
                        }
                    }
                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Debug.LogWarning($"[Singleton] Another instance of {typeof(T)} already exists! Destroying this duplicate.");
                Destroy(gameObject);
                return;
            }
            
            instance = this as T;
            
            if (!initialized)
            {
                OnSingletonCreated();
                initialized = true;
            }
        }

        protected virtual void OnSingletonCreated()
        {
            // Override trong derived class nếu cần
            DontDestroyOnLoad(gameObject);
            Debug.Log($"[Singleton] {typeof(T)} created and marked DontDestroyOnLoad.");
        }

        protected virtual void OnApplicationQuit()
        {
            isQuitting = true;
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                isQuitting = true;
            }
        }
    }
}
