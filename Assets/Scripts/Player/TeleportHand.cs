using UnityEngine;

public class TeleportHand : BaseHand
{
    [SerializeField] private LayerMask m_TeleportableMask;

    protected override void HandleHandInput()
    {
        Debug.DrawLine(transform.position, transform.position + transform.forward * float.MaxValue, Color.white, 5.0f);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, float.MaxValue, m_TeleportableMask))
        {
            Planet planet = raycastHit.transform.GetComponent<Planet>();
            if (Player.Instance.CurrPlanet == planet)
                return;
            planet.PlacePlayer(Player.Instance.transform);
            Player.Instance.SetPlanet(planet);
        }
    }
}
