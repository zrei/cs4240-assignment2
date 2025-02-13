using UnityEngine;

public class BulletCleanup : MonoBehaviour
{
    public float maxDistance = 30f; 
    private Transform playerTransform;


    void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.transform.position);

            if (distanceToPlayer > maxDistance)
            {
                gameObject.SetActive(false);


                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                }
            }
        }
    }
}
