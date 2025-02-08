using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private InputActionAsset m_InputActionAsset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void HandleAwake()
    {
        m_InputActionAsset.Enable();
    }

    protected override void HandleDestroy()
    {
        m_InputActionAsset.Disable();
    }
}
