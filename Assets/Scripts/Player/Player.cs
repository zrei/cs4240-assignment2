using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    [SerializeField] InputActionReference m_LookInput;
    [SerializeField] CanvasGroup m_BlackOutCg;

    public Planet CurrPlanet { get; private set; } = null;
    private Quaternion m_LookRotation;

    private Coroutine m_BlackOutCoroutine = null;

    public void SetPlanet(Planet planet)
    {
        CurrPlanet = planet;
    }

    private void Start()
    {
        m_LookInput.action.performed += OnLookUpdate;
        ToggleBlackOut(false);
    }

    private void OnDestroy()
    {
        m_LookInput.action.performed -= OnLookUpdate;
    }

    private void LateUpdate()
    {
        transform.rotation = m_LookRotation;
    }

    private void OnLookUpdate(InputAction.CallbackContext context)
    {
        m_LookRotation = context.ReadValue<Quaternion>();
    }

    private void ToggleBlackOut(bool enable)
    {
        if (m_BlackOutCoroutine != null)
        {
            StopCoroutine(m_BlackOutCoroutine);
            m_BlackOutCoroutine = null;
        }

        m_BlackOutCg.alpha = enable ? 1f : 0f;
        m_BlackOutCg.blocksRaycasts = enable;
        m_BlackOutCg.interactable = enable;
    }
}
