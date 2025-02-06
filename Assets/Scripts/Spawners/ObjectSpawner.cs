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
    [SerializeField] private float m_RadiusAroundSpawnerToSpawn;
    [SerializeField] private bool m_RestrictRadiusAroundObject;
    [Tooltip("Radius around each existing object that should not contain other objects")]
    [SerializeField] private float m_RestrictedRadiusAroundObject;

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
        spawnedObject.OnDestroyEvent += OnObjectDespawn;
        spawnedObject.transform.position = spawnPos;
        m_TrackedTransforms.Add(spawnedObject.transform);
    }

    #region Helper
    private Vector3 GetRandomPosition()
    {
        float x = (UnityEngine.Random.value * 2 - 1) * m_RadiusAroundSpawnerToSpawn;
        float y = 0;
        float z = (UnityEngine.Random.value * 2 - 1) * m_RadiusAroundSpawnerToSpawn;
        return transform.position + new Vector3(x, y, z);
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
        m_Pool.ReturnObjectToPool(spawnObject.gameObject);
        m_TrackedTransforms.Remove(spawnObject.transform);
        SpawnObject();
    }
}

public abstract class SpawnedObject : MonoBehaviour, IHeightHandle
{
    public Action<SpawnedObject> OnDestroyEvent;

    void IHeightHandle.OnHeightDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
}