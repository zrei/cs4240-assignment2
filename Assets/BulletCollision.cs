using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject explosionPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            ScoreManager.Instance.AddScore(1);
        }
    }
}
