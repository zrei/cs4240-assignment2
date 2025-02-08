using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    [SerializeField] private InputActionReference m_LookInput;
    [SerializeField] private CanvasGroup m_BlackOutCg;
    [SerializeField] private Camera m_PlayerCamera;

    [Header("Teleport Blackout Settings")]
    [SerializeField] private float m_FadeInTime = 0.5f;
    [SerializeField] private float m_FullBlackTime = 0.5f;
    [SerializeField] private float m_FadeOutTime = 0.5f;

    public Planet CurrPlanet { get; private set; } = null;
    private Quaternion m_LookRotation;

    private Coroutine m_BlackOutCoroutine = null;

    public event Action OnFadeInBeginEvent;
    public event Action OnFadeInCompleteEvent;
    public event Action OnFadeOutBeginEvent;
    public event Action OnFadeOutCompleteEvent;

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
        m_PlayerCamera.transform.localRotation = m_LookRotation;
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

    public void PerformTeleportBlackout(Action fadeInBeginAction = null, Action fadeInCompleteAction = null, Action fadeOutBeginAction = null, Action fadeOutCompleteAction = null)
    {
        if (m_BlackOutCoroutine != null)
        {
            StopCoroutine(m_BlackOutCoroutine);
        }
        m_BlackOutCoroutine = StartCoroutine(BlackoutCoroutine(fadeInBeginAction, fadeInCompleteAction, fadeOutBeginAction, fadeOutCompleteAction));
    }

    private IEnumerator BlackoutCoroutine(Action fadeInBeginAction = null, Action fadeInCompleteAction = null, Action fadeOutBeginAction = null, Action fadeOutCompleteAction = null)
    {
        float t = 0f;

        m_BlackOutCg.alpha = 0f;
        m_BlackOutCg.blocksRaycasts = true;
        m_BlackOutCg.interactable = true;

        OnFadeInBeginEvent?.Invoke();
        fadeInBeginAction?.Invoke();

        while (t < m_FadeInTime)
        {
            yield return null;
            t += Time.deltaTime;
            m_BlackOutCg.alpha = Mathf.Lerp(0f, 1f, t /  m_FadeInTime);
        }

        t = t % m_FadeInTime;

        OnFadeInCompleteEvent?.Invoke();
        fadeInCompleteAction?.Invoke();

        while (t < m_FullBlackTime)
        {
            yield return null;
            t += Time.deltaTime;
        }

        t = t % m_FullBlackTime;
        OnFadeOutBeginEvent?.Invoke();
        fadeOutBeginAction?.Invoke();

        while (t < m_FadeOutTime)
        {
            yield return null;
            t += Time.deltaTime;
            m_BlackOutCg.alpha = Mathf.Lerp(1f, 0f, t / m_FadeOutTime);
        }

        m_BlackOutCg.blocksRaycasts = false;
        m_BlackOutCg.interactable = false;

        OnFadeOutCompleteEvent?.Invoke();
        fadeOutCompleteAction?.Invoke();
    }
}
