using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Transform m_PlayerTeleportPoint;

    public void PlacePlayer(Transform playerTransform)
    {
        playerTransform.position = m_PlayerTeleportPoint.position;
        playerTransform.rotation = m_PlayerTeleportPoint.rotation;
    }

}
