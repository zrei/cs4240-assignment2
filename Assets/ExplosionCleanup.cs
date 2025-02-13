using UnityEngine;

public class ExplosionCleanup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (particleSystem && !particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
