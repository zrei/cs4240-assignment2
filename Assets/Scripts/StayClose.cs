using UnityEngine;

public class StayClose : MonoBehaviour
{
    public float maxDistance = 6f;
    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent;

        if (parentTransform == null)
        {
            Debug.LogWarning("StayClose script requires a parent object!");
        }
    }

    void LateUpdate()
    {
        if (parentTransform != null)
        {

            Vector3 directionToParent = parentTransform.position - transform.position;


            if (directionToParent.magnitude > maxDistance)
            {
                Vector3 clampedPosition = parentTransform.position - directionToParent.normalized * maxDistance;
                transform.position = clampedPosition;
            }
        }
    }
}
