using UnityEngine;

public class Asteroid : SpawnedObject
{
    // upon "despawning" the projectile, please call OnDestroyEvent?.Invoke(this)

    [SerializeField] private GameObject[] m_Models;
    private Fracture m_AsteroidFracture;

    private void Start()
    {
        SpawnRandomModel();
        m_AsteroidFracture = GetComponentInChildren<Fracture>();
    }

    private void SpawnRandomModel()
    {
        GameObject model = Instantiate(m_Models[UnityEngine.Random.Range(0, m_Models.Length)]);
        model.transform.parent = transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;
    }
}
