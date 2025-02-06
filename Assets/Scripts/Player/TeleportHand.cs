using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TeleportHand : BaseHand
{
    [SerializeField] private LayerMask m_TeleportableMask;
    [SerializeField] private float m_TeleportCooldown = 5f;
    [SerializeField] private AudioClip m_TeleportClip;

    private float m_CurrCooldown = 0f;

    private AudioSource m_AudioSource;

    protected override void Start()
    {
        base.Start();
        m_AudioSource = GetComponent<AudioSource>();
    }

    protected override void HandleHandInput()
    {
        if (m_CurrCooldown > 0)
            return;
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, float.MaxValue, m_TeleportableMask))
        {
            Planet planet = raycastHit.transform.GetComponent<Planet>();
            if (Player.Instance.CurrPlanet == planet)
                return;
            planet.PlacePlayer(Player.Instance.transform);
            Player.Instance.SetPlanet(planet);
        }

        m_CurrCooldown = m_TeleportCooldown;
        m_AudioSource.PlayOneShot(m_TeleportClip);
    }

    private void Update()
    {
        if (m_CurrCooldown > 0)
            m_CurrCooldown -= Time.deltaTime;
        if (m_CurrCooldown <= 0)
            Debug.DrawLine(transform.position, transform.position + transform.forward * float.MaxValue, Color.white, 5.0f);
    }
}
