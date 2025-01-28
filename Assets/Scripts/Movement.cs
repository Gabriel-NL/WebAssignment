using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private Vector3 currentMoveDirection = Vector3.zero; // Store the current move direction
    private Vector2 moveInput = Vector2.zero; // Store the input value
    private bool isMoving = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found on this GameObject.");
            return;
        }

        moveAction = playerInput.actions.FindAction("Move");
        if (moveAction == null)
        {
            Debug.LogError("Move action not found in the Input Actions Asset.");
            return;
        }
    }

    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMoveCanceled;
    }

   
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = true; // Set to true when movement starts
        Debug.Log("Moving started"); // For debugging
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        isMoving = false; // Set to false when movement stops
        Debug.Log("Moving stopped"); // For debugging
    }

    private void Update()
    {
        // Update move direction based on player's forward vector
        currentMoveDirection = transform.forward * moveInput.y + transform.right * moveInput.x;

        if (isMoving) // Only move if isMoving is true
        {
            transform.Translate(currentMoveDirection * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}