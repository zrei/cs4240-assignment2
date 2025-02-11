using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab; 
    [SerializeField] private int initialAsteroidCount = 5; 
    [SerializeField] private float spawnRange = 10f; 
    public void Start()
    {
        SpawnAsteroids(initialAsteroidCount);
    }

    private void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = GenerateRandomPosition();
            Instantiate(asteroidPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float y = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);

        return new Vector3(x, y, z);
    }
}
