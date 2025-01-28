using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CamMovement : MonoBehaviour
{
    public CinemachineCamera cam_cine;
    public float sensitivity = 3.0f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private PlayerInput playerInput;
    public GameObject player;
    public GameObject cam;
    private InputAction lookAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found on this GameObject.");
            return;
        }

        lookAction = playerInput.actions.FindAction("Look"); 
        if (lookAction == null)
        {
            Debug.LogError("Look action not found in the Input Actions Asset.");
            return;
        }

        lookAction.performed += OnLook;

    }

 

    private void OnDestroy()
    {
        //Unsubscribe from events to prevent memory leaks
        if (lookAction != null)
        {
            lookAction.performed -= OnLook;
            // lookAction.started -= OnLookStarted;
            // lookAction.canceled -= OnLookCanceled;
        }
    }

    private void EnableLookAction()
    {
        if(lookAction != null && !lookAction.enabled)
        {
            lookAction.Enable();
        }
    }

    private void DisableLookAction()
    {
        if (lookAction != null && lookAction.enabled)
        {
            lookAction.Disable();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 lookDelta = context.ReadValue<Vector2>();
        float mouseX = lookDelta.x * sensitivity;
        float mouseY = lookDelta.y * sensitivity;

        // Corrected Player Rotation:
        player.transform.Rotate(Vector3.up * mouseX); //

        // Rotate Camera (Vertical Movement)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //Apply rotation to the camera transform
        cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // Apply only X rotation
    }


}