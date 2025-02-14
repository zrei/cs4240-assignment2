using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool m_Pool;

    [Header("Spawn")]
    [SerializeField] private int m_AmountToSpawn;
    [Tooltip("Radius around the spawner to spawn objects")]
    [SerializeField] private float m_MinimumRadiusToSpawn = 0;
    [SerializeField] private float m_MaximumRadiusToSpawn; // 30 and 130
    [SerializeField] private bool m_RestrictRadiusAroundObject;
    [Tooltip("Radius around each existing object that should not contain other objects")]
    [SerializeField] private float m_RestrictedRadiusAroundObject;
    [SerializeField] private Vector3 m_ScaleValue = Vector3.one;

    private HashSet<Transform> m_TrackedTransforms = new();

    private void Start()
    {
        for (int i = 0; i < m_AmountToSpawn; ++i)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector3 spawnPos = GetRandomPosition();
        if (m_RestrictRadiusAroundObject)
        {
            while (HasOverlappingObjects(spawnPos))
            {
                spawnPos = GetRandomPosition();
            }
        }

        SpawnedObject spawnedObject = m_Pool.GetObject<SpawnedObject>(true);
        spawnedObject.Prepare();
        spawnedObject.OnDestroyEvent += OnObjectDespawn;
        spawnedObject.transform.position = spawnPos;
        spawnedObject.transform.localScale = m_ScaleValue;
        m_TrackedTransforms.Add(spawnedObject.transform);
    }

    #region Helper
    private Vector3 GetRandomPosition()
    {
        float radius = UnityEngine.Random.value * (m_MaximumRadiusToSpawn - m_MinimumRadiusToSpawn) + m_MinimumRadiusToSpawn;
        float angle = UnityEngine.Random.value * 360;
        return transform.position + Quaternion.AngleAxis(angle, transform.up) * transform.forward * radius;
    }

    private bool HasOverlappingObjects(Vector3 pos)
    {
        foreach (Transform fruitTransform in m_TrackedTransforms)
        {
            if (IsOverlapping(pos, fruitTransform))
                return true;
        }
        return false;
    }

    private bool IsOverlapping(Vector3 pos, Transform fruitTransform)
    {
        return (pos - fruitTransform.position).magnitude <= m_RestrictedRadiusAroundObject;
    }
    #endregion

    private void OnObjectDespawn(SpawnedObject spawnObject)
    {
        spawnObject.OnDestroyEvent -= OnObjectDespawn;
        m_Pool.ReturnObjectToPool(spawnObject.gameObject);
        m_TrackedTransforms.Remove(spawnObject.transform);
        SpawnObject();
    }
}

[RequireComponent(typeof(Rigidbody))]
public abstract class SpawnedObject : MonoBehaviour, IHeightHandle
{
    public event Action<SpawnedObject> OnDestroyEvent;

    protected Rigidbody m_Rigidbody;

    void IHeightHandle.OnHeightDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }

    public virtual void Prepare() 
    { 
        if (m_Rigidbody == null)
            m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.linearVelocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }

    public void Despawn()
    { 
        OnDestroyEvent?.Invoke(this);
    }

}