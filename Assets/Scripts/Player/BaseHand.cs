using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseHand : MonoBehaviour
{
    [SerializeField] private InputActionReference m_InputAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        m_InputAction.action.performed += HandleHandInput;
    }

    private void OnDestroy()
    {
        m_InputAction.action.performed -= HandleHandInput;
    }

    private void HandleHandInput(InputAction.CallbackContext context)
    {
        HandleHandInput();
    }

    protected abstract void HandleHandInput();
}
