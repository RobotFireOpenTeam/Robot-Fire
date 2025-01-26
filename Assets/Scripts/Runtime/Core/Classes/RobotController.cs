using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RobotController : NetworkBehaviour 
{
    [Header("Objects")]
    [SerializeField] protected CharacterController _characterController;
    [SerializeField] protected Camera _mainCam;
    [SerializeField] protected AudioListener _audioListener;
    [SerializeField] protected Weapon _weapon;

    [SerializeField] protected float _upDownRange = 90f;
    protected float _verticalRotation;
    
    [Header("Movement")]
    [SerializeField] protected float _walkSpeed = 5.0f;
    [SerializeField] protected float _sprintMultiplier = 5.0f;
    protected float _speedMultiplier = 1f;
    
    [Header("Gravity / JumpForce")]
    [SerializeField] protected float _gravity = 9.81f;
    [SerializeField] protected float _jumpForce = 5f;
    
    [Header("Look Sensitivity")]
    [SerializeField] protected float _lookSensitivity = 3.0f;
    protected bool _cursorIsLocked = true;

    [Header("Input Actions")]
    [SerializeField] protected PlayerInput _playerInput;

    protected Vector2 _moveInput;
    protected Vector2 _lookInput;

    protected bool _isMoving;
    protected Vector3 _currentMovement = Vector3.zero;
    
    [Header("Developer Console")]
    [SerializeField] protected DeveloperConsoleUI devConsole;
    protected bool localDeveloperConsoleOpened = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _audioListener = GetComponentInChildren<AudioListener>();
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
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Running");
            _speedMultiplier = _sprintMultiplier;
        }
        else
        {
            _speedMultiplier = 1f;
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

        _playerInput.enabled = IsOwner;
        _characterController.enabled = IsOwner;
        _mainCam.enabled = IsOwner;
        _audioListener.enabled = IsOwner;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} spawned with OwnerClientId: {OwnerClientId}");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        _playerInput.enabled = false;
        _characterController.enabled = false;
        _mainCam.enabled = false;
        _audioListener.enabled = false;

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
        float verticalSpeed     = _moveInput.y * _walkSpeed * _speedMultiplier;
        float horizontalSpeed   = _moveInput.x * _walkSpeed * _speedMultiplier;

        Vector3 horizontalMovement = new(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        handleGravityAndJumping();

        _currentMovement.x = horizontalMovement.x;
        _currentMovement.z = horizontalMovement.z;
        
        _characterController.Move(_currentMovement * Time.deltaTime);

        _isMoving = _moveInput.y != 0 || _moveInput.x != 0;
    }

    protected void handleGravityAndJumping()
    {
        if (_characterController.isGrounded)
        {
            _currentMovement.y = -0.5f;
        }
        else
        {
            _currentMovement.y -= _gravity * Time.deltaTime;
        }
    }

    protected void HandleRotation()
    {
        float mouseXRotation = _lookInput.x * _lookSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        _verticalRotation -= _lookInput.y * _lookSensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_upDownRange, _upDownRange);
        _mainCam.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
    }

    //controls the locking and unlocking of the mouse
    protected void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _cursorIsLocked = true;
        }

        if (_cursorIsLocked)
        {
            UnlockCursor();
        }
        else if (!_cursorIsLocked)
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
        if (_characterController.isGrounded)
        {
            Debug.Log($"Jump triggered. Owner: {OwnerClientId}");
        }
    }

    [ServerRpc]
    protected void ShootServerRPC()
    {
        Debug.Log($"Shooting triggered. Owner: {OwnerClientId}");
        _weapon.Shooting();
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
