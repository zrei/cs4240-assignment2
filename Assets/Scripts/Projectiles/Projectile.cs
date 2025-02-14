using UnityEngine;

public class Projectile : SpawnedObject
{
    [SerializeField] private GameObject[] m_Models;

    protected virtual void Start()
    {
        GameObject model = Instantiate(GetRandomModel());
        model.transform.parent = transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;
    }

    private GameObject GetRandomModel()
    {
        return m_Models[Random.Range(0, m_Models.Length)];
    }
}
