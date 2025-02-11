using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool projectilePool;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float horizontalSpeed = 5f; 
    [SerializeField] private float verticalSpeed = 5f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            spawnPoint.position += Vector3.left * horizontalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            spawnPoint.position += Vector3.right * horizontalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            spawnPoint.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            spawnPoint.position += Vector3.down * verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnProjectile();
        }
    }


    private void SpawnProjectile()
    {
        if (projectilePool == null || spawnPoint == null)
        {
            Debug.LogError("Projectile Pool or Spawn Point is not assigned.");
            return;
        }

        GameObject projectile = projectilePool.GetObject(true, null);
        if (projectile != null)
        {
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.transform.forward = spawnPoint.forward;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = spawnPoint.forward * projectileSpeed;
            }
        }
    }
}

