using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] protected Camera mainCam;

    [SerializeField] private float upDownRange = 90f;
    private float verticalRotation;
    
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 5.0f;

    
    [Header("Gravity / JumpForce")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Look Sensitivity")]
    [SerializeField] private float m_lookSensitivity = 3.0f;
    private bool m_cursorIsLocked = true;

    [Header("Input Actions")]
    [SerializeField] private InputActionAsset playerControls;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction attackAction;
    private InputAction interactAction;
    private InputAction crouchAction;
    private InputAction jumpAction;
    private InputAction previousAction;
    private InputAction nextAction;
    private InputAction sprintAction;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private bool isMoving;
    private Vector3 currentMovement = Vector3.zero;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        moveAction = playerControls.FindActionMap("Player").FindAction("Move");
        lookAction = playerControls.FindActionMap("Player").FindAction("Look");
        sprintAction = playerControls.FindActionMap("Player").FindAction("Sprint");
        jumpAction = playerControls.FindActionMap("Player").FindAction("Jump");

        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        sprintAction.Enable();
        jumpAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        sprintAction.Disable();
        jumpAction.Disable();
    }


    private void Update()
    {
        HandleMovement();
        HandleRotation();
        InternalLockUpdate();
    }

    private void HandleMovement()
    {
        float speedMultiplier = sprintAction.ReadValue<float>() > 0 ? sprintMultiplier : 1f;

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

    private void handleGravityAndJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;

            if (jumpAction.triggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        float mouseXRotation = lookInput.x * m_lookSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= lookInput.y * m_lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    //controls the locking and unlocking of the mouse
    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            UnlockCursor();
        }
        else if (!m_cursorIsLocked)
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
}
