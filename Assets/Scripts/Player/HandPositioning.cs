using UnityEngine;
using UnityEngine.InputSystem;

public class HandPositioning : MonoBehaviour
{
    [SerializeField] private InputActionReference m_PositionInputAction;
    [SerializeField] private InputActionReference m_RotationInputAction;

    private Vector3 m_LocalPosition;
    private Quaternion m_LocalRotation;

    public Vector3 LinearVelocity { get; private set; }
    public Vector3 AngularVelocity { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_PositionInputAction.action.performed += OnControllerPositionUpdate;
        m_RotationInputAction.action.performed += OnControllerRotationUpdate;

        LinearVelocity = Vector3.zero;
        AngularVelocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        m_PositionInputAction.action.performed -= OnControllerPositionUpdate;
        m_RotationInputAction.action.performed -= OnControllerRotationUpdate;
    }

    // Update is called once per frame
    private void Update()
    {
        LinearVelocity = m_LocalPosition - transform.localPosition;
        Quaternion derived = m_LocalRotation * Quaternion.Inverse(transform.localRotation);
        float angleInDegrees;
        Vector3 rotationAxis;
        derived.ToAngleAxis(out angleInDegrees, out rotationAxis);
        AngularVelocity = rotationAxis * angleInDegrees * Mathf.Deg2Rad / Time.deltaTime;
        transform.localPosition = m_LocalPosition;
        transform.localRotation = m_LocalRotation;
    }

    private void OnControllerPositionUpdate(InputAction.CallbackContext context)
    {
        m_LocalPosition = context.ReadValue<Vector3>();
    }

    private void OnControllerRotationUpdate(InputAction.CallbackContext context)
    {
        m_LocalRotation = context.ReadValue<Quaternion>();
    }
}
