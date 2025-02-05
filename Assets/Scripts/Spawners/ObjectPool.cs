using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjectToPool;
    [SerializeField] private int m_InitialAmountToSpawn;

    private List<GameObject> m_PooledObjects = new();

    private void Start()
    {
        for (int i  = 0; i < m_InitialAmountToSpawn; ++i)
        {
            CreateNewObject();
        }
    }

    public GameObject GetObject(bool setActive = false, Transform newParent = null)
    {
        GameObject obj = null;

        if (m_PooledObjects.Count == 0)
        {
            obj = CreateNewObject();
        }
        else
        {
            obj = m_PooledObjects[0];
            m_PooledObjects.RemoveAt(0);
        }

        if (setActive)
            obj.SetActive(true);

        obj.transform.parent = newParent;

        return obj;
    }

    public T GetObject<T>(bool setActive = false, Transform newParent = null)
    {
        return GetObject(setActive, newParent).GetComponent<T>();
    }    

    public void ReturnObjectToPool(GameObject obj)
    {
        m_PooledObjects.Add(obj);
        obj.SetActive(false);
        obj.transform.parent = transform;
    }

    #region Helper
    private GameObject CreateNewObject()
    {
        GameObject newObj = Instantiate(m_ObjectToPool, Vector3.zero, Quaternion.identity, transform);
        newObj.SetActive(false);
        return newObj;
    }
    #endregion
}
