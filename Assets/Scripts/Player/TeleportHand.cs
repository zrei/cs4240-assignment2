using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TeleportHand : BaseHand
{
    [SerializeField] private LayerMask m_TeleportableMask;
    [SerializeField] private float m_TeleportCooldown = 5f;
    [SerializeField] private AudioClip m_TeleportClip;
    [SerializeField] private LineRenderer m_LineRenderer;
    [SerializeField] private float m_MaxDrawDistance = 20f;

    private float m_CurrCooldown = 0f;

    private AudioSource m_AudioSource;

    private RaycastHit m_CurrHitData;
    private bool m_HasHit;

    protected override void Start()
    {
        base.Start();
        m_AudioSource = GetComponent<AudioSource>();
        m_LineRenderer.enabled = true;
    }

    protected override void HandleHandInput()
    {
        if (m_CurrCooldown > 0)
            return;

        if (!m_HasHit)
            return;

        Planet planet = m_CurrHitData.transform.GetComponent<Planet>();
        if (Player.Instance.CurrPlanet == planet)
            return;

        Player.Instance.PerformTeleportBlackout(null, () => TeleportPlayer(planet));

        m_CurrCooldown = m_TeleportCooldown;
        m_LineRenderer.enabled = false;
    }

    private void Update()
    {
        if (m_CurrCooldown > 0)
        {
            m_CurrCooldown -= Time.deltaTime;
            if (m_CurrCooldown <= 0)
                m_LineRenderer.enabled = true;
        }
            
        if (m_CurrCooldown <= 0)
        {
            m_HasHit = Physics.Raycast(transform.position, transform.forward, out m_CurrHitData, float.MaxValue, m_TeleportableMask);
            m_LineRenderer.SetPosition(0, transform.position);
            m_LineRenderer.SetPosition(1, m_HasHit ? m_CurrHitData.point : transform.position + transform.forward * m_MaxDrawDistance);
        }
    }

    private void TeleportPlayer(Planet planet)
    {
        planet.PlacePlayer(Player.Instance.transform);
        Player.Instance.SetPlanet(planet);
        m_AudioSource.PlayOneShot(m_TeleportClip);
    }
}
