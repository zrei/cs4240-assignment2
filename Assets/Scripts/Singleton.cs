using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T m_Instance = null;
    public static T Instance => m_Instance;
    public static bool IsReady => Instance != null;

    public static Action OnReady;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("An instance of " + typeof(T) + " already exists!");
            Destroy(this.gameObject);
            return;
        }

        HandleAwake();
        m_Instance = this.GetComponent<T>();
        OnReady?.Invoke();
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        HandleDestroy();
        m_Instance = null;
    }

    protected virtual void HandleAwake() { }

    protected virtual void HandleDestroy() { }
}
