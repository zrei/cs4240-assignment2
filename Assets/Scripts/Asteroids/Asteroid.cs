using UnityEngine;

public class Asteroid : SpawnedObject
{
    // upon "despawning" the projectile, please call OnDestroyEvent?.Invoke(this) instead of calling Destroy(gameObject)

    [SerializeField] private GameObject[] m_Models;
    

    [SerializeField] private float m_MinMass;
    [SerializeField] private float m_MaxMass;

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

    public override void Prepare()
    {
        base.Prepare();
        m_Rigidbody.mass = Random.value * (m_MaxMass - m_MinMass) + m_MinMass;
    }
}
