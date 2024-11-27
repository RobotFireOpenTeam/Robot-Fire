using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RobotController : NetworkBehaviour
{
    [Header("Objects")]
    [SerializeField] protected CharacterController characterController;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected AudioListener audioListener;
    [SerializeField] protected Weapon weapon;

    [SerializeField] protected float upDownRange = 90f;
    protected float verticalRotation;
    
    [Header("Movement")]
    [SerializeField] protected float walkSpeed = 5.0f;
    [SerializeField] protected float sprintMultiplier = 5.0f;
    protected float speedMultiplier = 1f;
    
    [Header("Gravity / JumpForce")]
    [SerializeField] protected float gravity = 9.81f;
    [SerializeField] protected float jumpForce = 5f;
    
    [Header("Look Sensitivity")]
    [SerializeField] protected float lookSensitivity = 3.0f;
    protected bool cursorIsLocked = true;

    [Header("Input Actions")]
    [SerializeField] protected PlayerInput playerInput;

    protected Vector2 moveInput;
    protected Vector2 lookInput;

    protected bool isMoving;
    protected Vector3 currentMovement = Vector3.zero;
    
    [Header("Developer Console")]
    [SerializeField] protected DeveloperConsoleUI devConsole;
    protected bool localDeveloperConsoleOpened = false;


    // Network Variables
    [Header("Network Variables")]
    protected NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>();
    protected NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>();

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        audioListener = GetComponentInChildren<AudioListener>();
    }

    // PlayerInput Events

    public void OnConsoleButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Console");
            if (localDeveloperConsoleOpened) {
                localDeveloperConsoleOpened = false;
                devConsole.CloseConsole();
            }
            else {
                localDeveloperConsoleOpened = true;
                devConsole.OpenConsole();
            }
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Running");
            speedMultiplier = sprintMultiplier;
        }
        else
        {
            speedMultiplier = 1f;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpServerRPC();
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootServerRPC();
        }
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            AimServerRPC();
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CrouchServerRPC();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractServerRPC();
        }
    }

    // Network Spawn & Despawn
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        playerInput.enabled = IsOwner;
        characterController.enabled = IsOwner;
        mainCam.enabled = IsOwner;
        audioListener.enabled = IsOwner;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} spawned with OwnerClientId: {OwnerClientId}");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        playerInput.enabled = false;
        characterController.enabled = false;
        mainCam.enabled = false;
        audioListener.enabled = false;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} despawned");
    }

    private void Update()
    {
        if (!IsOwner) 
        {
            return;
        }

        HandleMovement();
        HandleRotation();
        InternalLockUpdate();
    }

    protected void HandleMovement()
    {
        float verticalSpeed = moveInput.y * walkSpeed * speedMultiplier;
        float horizontalSpeed = moveInput.x * walkSpeed * speedMultiplier;

        Vector3 horizontalMovement = new Vector3 (horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        handleGravityAndJumping();

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;
        
        characterController.Move(currentMovement * Time.deltaTime);

        isMoving = moveInput.y != 0 || moveInput.x != 0;
    }

    protected void handleGravityAndJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    protected void HandleRotation()
    {
        float mouseXRotation = lookInput.x * lookSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= lookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    //controls the locking and unlocking of the mouse
    protected void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorIsLocked = true;
        }

        if (cursorIsLocked)
        {
            UnlockCursor();
        }
        else if (!cursorIsLocked)
        {
            LockCursor();
        }
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Networking Staff

    [ServerRpc]
    protected void JumpServerRPC()
    {
        if (characterController.isGrounded)
        {
            Debug.Log($"Jump triggered. Owner: {OwnerClientId}");
        }
    }

    [ServerRpc]
    protected void ShootServerRPC()
    {
        Debug.Log($"Shooting triggered. Owner: {OwnerClientId}");
    }

    [ServerRpc]
    protected void AimServerRPC()
    {
        Debug.Log($"Aim triggered. Owner: {OwnerClientId}");
    }

    [ServerRpc]
    protected void CrouchServerRPC()
    {
        Debug.Log($"Crouch triggered. Owner: {OwnerClientId}");
    }

    [ServerRpc]
    protected void InteractServerRPC()
    {
        Debug.Log($"Interact triggered. Owner: {OwnerClientId}");
    }
}
