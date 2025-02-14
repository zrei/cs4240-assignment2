using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Custom_GrabInteractable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Input Actions")]
    public InputActionReference grabTriggerAction;

    [Header("Grab Settings")]
    public float grabThreshold = 0.1f;
    public float moveSpeed = 10f;

    private bool isGrabbing = false;
    private bool isGripPressed = false;
    private GameObject grabbedObject;

    [Header("Visual References")]
    [SerializeField] private LineRenderer rayLine; 
    public Color rayColor = Color.yellow;

    private void Awake()
    {
        if (rayLine != null)
        {
            rayLine.startWidth = 0.005f;
            rayLine.endWidth = 0.005f;
            rayLine.startColor = rayColor;
            rayLine.endColor = rayColor;
        }
        else
        {
            Debug.LogError("Ray Line reference missing!");
        }

    }

    private void OnEnable()
    {

        if (grabTriggerAction == null)
        {
            Debug.LogError("Grab trigger action not assigned!");
            return;
        }
        grabTriggerAction.action.Enable();

        grabTriggerAction.action.performed += OnGrabTriggerPerformed;
        grabTriggerAction.action.canceled += OnGrabTriggerCanceled;
    }

    private void Update()
    {
        if (isGripPressed && !isGrabbing)
        {
            ShowGrabLine();
        }

        if (isGrabbing && grabbedObject != null)
        {
            grabbedObject.transform.position = Vector3.Lerp(
                grabbedObject.transform.position,
                transform.position,
                moveSpeed * Time.deltaTime
            );

            grabbedObject.transform.rotation = Quaternion.Lerp(
                grabbedObject.transform.rotation,
                transform.rotation * Quaternion.Euler(0, -90,0),
                moveSpeed * Time.deltaTime
            );
        }
    }

    private void OnDisable()
    {
        grabTriggerAction.action.Disable();

        grabTriggerAction.action.performed -= OnGrabTriggerPerformed;
        grabTriggerAction.action.canceled -= OnGrabTriggerCanceled;
    }


    private void OnGrabTriggerPerformed(InputAction.CallbackContext context)
    {
        float grabTriggerValue = context.ReadValue<float>();
        isGripPressed = true;
        TryGrabObject();
        Debug.LogWarning("Pressing Grab now");
    }

    private void OnGrabTriggerCanceled(InputAction.CallbackContext context)
    {
        isGripPressed = false;
        if (isGrabbing)
        {
            ReleaseObject();
        }
        if (rayLine != null) rayLine.positionCount = 0;
        Debug.LogWarning("Releasing Grab now");
    }

    private void ShowGrabLine()
    {
        RaycastHit hit;
        float rayLength = 20f;

        if (rayLine != null)
        {
            rayLine.positionCount = 2;
            rayLine.SetPosition(0, transform.position);
            rayLine.SetPosition(1, transform.position + transform.forward * rayLength);
        }
    }

    private void TryGrabObject()
    {
        Debug.Log("Attempting to grab object...");

        if (rayLine == null)
        {
            Debug.LogError("LineRenderers not initialized!");
            return;
        }
        RaycastHit hit;
        float rayLength = 100f;


        if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");


            if (hit.collider.CompareTag("Grabbable"))
            {
                Debug.Log("Grabbable object detected!");

                grabbedObject = hit.collider.gameObject;

                if (hit.collider.gameObject.layer != LayerMask.NameToLayer("PickUp"))
                {
                    grabbedObject.transform.SetParent(transform);
                    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    isGrabbing = true;
                }
                else
                {
                    grabbedObject.GetComponent<ProjectilePickUp>().PickUp();
                    grabbedObject = null;
                }
            }
            else
            {
                Debug.Log("Hit object is not tagged as Grabbable.");
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }



    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null); 
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; 
            grabbedObject = null;
            isGrabbing = false;
        }
    }

}