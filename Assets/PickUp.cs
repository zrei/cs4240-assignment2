using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("RightHand").transform;
    }

    void Update()
    {
        if (transform.parent == playerTransform)
        {
            ScoreManager.Instance.AddScore(5);
            Destroy(gameObject) // Needs to be however it returns to pool
            
        }
    }
}
