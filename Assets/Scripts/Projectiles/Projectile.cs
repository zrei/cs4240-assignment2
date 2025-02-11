using UnityEngine;

public class Projectile : SpawnedObject
{
    [SerializeField] private GameObject[] m_Models;
    [SerializeField] private float m_Speed = 20f;
    private Rigidbody rb;

    public void ResetProjectile()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * m_Speed, ForceMode.Impulse); 
        }

        GameObject model = Instantiate(GetRandomModel());
        model.transform.parent = transform;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        model.transform.localScale = Vector3.one;

        Launch();
    }

    private GameObject GetRandomModel()
    {
        return m_Models[Random.Range(0, m_Models.Length)];
    }

    private void Launch()
    {
        if (rb != null)
        {
            Debug.Log("Projectile launched with forward direction: " + transform.forward);
            rb.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
        }            

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collided with: " + collision.gameObject.name);

        if (collision.gameObject.TryGetComponent(out Asteroid asteroid))
        {
            Debug.Log("Asteroid hit!");
            asteroid.TakeDamage(1);

            ScoreManager.Instance.AddScore(1);

            InvokeOnDestroyEvent();
        }
        else
        {
            InvokeOnDestroyEvent();
        }
    }

    private System.Collections.IEnumerator DestroyAsteroidAfterDelay(Asteroid asteroid, float delay)
    {
        Debug.Log("Starting destruction delay for asteroid.");
        yield return new WaitForSeconds(delay);
        if (asteroid != null)
        {
            Debug.Log("Asteroid destroyed after delay!");
            asteroid.DestroyAsteroid(); 
        }
    }

}
