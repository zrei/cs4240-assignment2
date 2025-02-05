using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputActionAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_InputActionAsset.Enable();
    }

    private void OnDestroy()
    {
        m_InputActionAsset.Disable();
    }
}
