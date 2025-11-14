using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubScripts.Singleton
{
    #region Basic Singleton
    /// <summary>
    /// Singleton cơ bản
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
    #endregion

    #region Singleton Through Scene

    /// <summary>
    /// Singleton dùng cho trường hợp không muốn destroy khi load scene 
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
    #endregion

    #region Singleton for Subclass 

    /// <summary>
    /// Singleton cho phép các subclass kế thừa từ parent singleton class 
    /// </summary>
    public abstract class SingletonSubclass : MonoBehaviour
    {
        private static Dictionary<Type, SingletonSubclass> instances = new();
        private static bool isQuitting = false;

        protected static T GetInstance<T>() where T : SingletonSubclass
        {
            Type type = typeof(T);

            if (isQuitting)
                return null;

            // Nếu đã có
            if (instances.TryGetValue(type, out SingletonSubclass exist))
                return (T)exist;

            // Tìm trong scene
            var found = FindObjectOfType(type) as SingletonSubclass;
            if (found != null)
            {
                instances[type] = found;
                return (T)found;
            }

            // Tạo mới
            GameObject go = new GameObject($"[Singleton] {type.Name}");
            var inst = go.AddComponent(type) as SingletonSubclass;
            instances[type] = inst;

            return (T)inst;
        }

        protected virtual void OnDestroy()
        {
            instances.Remove(GetType());
        }

        protected virtual void OnApplicationQuit()
        {
            isQuitting = true;
        }
    }

    #endregion
}



