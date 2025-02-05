using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ObjectPool m_Pool;

    [Header("Fruit Spawn")]
    [SerializeField] private int m_AmountToSpawn;
    [Tooltip("Radius around the spawner to spawn objects")]
    [SerializeField] private float m_RadiusAroundSpawnerToSpawn;
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
        while (HasOverlappingObjects(spawnPos))
        {
            spawnPos = GetRandomPosition();
        }
        T gameObject = m_Pool.GetObject<T>(true);
        gameObject.transform.position = spawnPos;
        m_TrackedTransforms.Add(gameObject.transform);
    }

    #region Helper
    private Vector3 GetRandomPosition()
    {
        float x = (Random.value * 2 - 1) * m_RadiusAroundSpawnerToSpawn;
        float y = 0;
        float z = (Random.value * 2 - 1) * m_RadiusAroundSpawnerToSpawn;
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
}
