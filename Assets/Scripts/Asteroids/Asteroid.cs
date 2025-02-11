using UnityEngine;

public class Asteroid : SpawnedObject
{
    [SerializeField] private GameObject[] m_Models;
    

    [SerializeField] private float m_MinMass;
    [SerializeField] private float m_MaxMass;
    [SerializeField] private int health = 3;  

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

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Asteroid took damage. Remaining health: " + health);
        if (health <= 0)
        {
            DestroyAsteroid(); 
        }
    }

    public void DestroyAsteroid()
    {
        Debug.Log("DestroyAsteroid method called.");
        
        if (m_AsteroidFracture != null)
        {
            Debug.Log("Asteroid destroyed!");
            m_AsteroidFracture.FractureObject();
        }
        InvokeOnDestroyEvent(); 
    }


}
