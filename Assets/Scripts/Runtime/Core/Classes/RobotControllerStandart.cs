using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RobotControllerStandart : NetworkBehaviour
{
    [Header("Objects")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected Camera mainCam;
    [SerializeField] private Weapon weapon;

    [SerializeField] private float upDownRange = 90f;
    private float verticalRotation;
    
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 5.0f;
    float speedMultiplier = 1f;
    
    [Header("Gravity / JumpForce")]
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Look Sensitivity")]
    [SerializeField] private float m_lookSensitivity = 3.0f;
    private bool m_cursorIsLocked = true;

    [Header("Input Actions")]
    [SerializeField] private PlayerInput playerInput;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private bool isMoving;
    private Vector3 currentMovement = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        rb.isKinematic = true;
    }

//     // PlayerInput Events

//     public void OnConsoleButtonPressed(InputAction.CallbackContext context)
//     {
//         if (context.performed)
//         {
//             Debug.Log("Console");
//             if (LocalDeveloperConsoleOpened) {
//                 LocalDeveloperConsoleOpened = false;
//                 devConsole.CloseConsole();
//             }
//             else {
//                 LocalDeveloperConsoleOpened = true;
//                 devConsole.OpenConsole();
//             }
//         }
//     }

//     public void OnMove(InputAction.CallbackContext context)
//     {
//         moveInput = context.ReadValue<Vector2>();
//     }

//     public void OnLook(InputAction.CallbackContext context)
//     {
//         lookInput = context.ReadValue<Vector2>();
//     }

//     public void OnSprint(InputAction.CallbackContext context)
//     {
//         if (context.performed)
//         {
//             Debug.Log("Running");
//             speedMultiplier = sprintMultiplier;
//         }
//         else
//         {
//             speedMultiplier = 1f;
//         }
//     }

//     public void OnJump(InputAction.CallbackContext context)
//     {
//         if (context.performed)
//         {
//             // JumpServerRPC();
//         }
//     }

//     public void OnAttack(InputAction.CallbackContext context)
//     {
//         if (context.performed)
//         {
//             // ShootServerRPC();
//         }
//     }
//     private void Update()
//     {
//         if (!IsOwner) 
//         {
//             return;
//         }
        
//         HandleRotation();
//     }

//     private void HandleMovement()
//     {
//         float verticalSpeed = moveInput.y * walkSpeed * speedMultiplier;
//         float horizontalSpeed = moveInput.x * walkSpeed * speedMultiplier;

//         Vector3 horizontalMovement = new Vector3 (horizontalSpeed, 0, verticalSpeed);
//         horizontalMovement = transform.rotation * horizontalMovement;

//         handleGravityAndJumping();

//         currentMovement.x = horizontalMovement.x;
//         currentMovement.z = horizontalMovement.z;
        
//         characterController.Move(currentMovement * Time.deltaTime);

//         isMoving = moveInput.y != 0 || moveInput.x != 0;
//     }

//     private void handleGravityAndJumping()
//     {
//         if (characterController.isGrounded)
//         {
//             currentMovement.y = -0.5f;
//         }
//         else
//         {
//             currentMovement.y -= gravity * Time.deltaTime;
//         }
//     }

//     void HandleRotation()
//     {
//         float mouseXRotation = lookInput.x * m_lookSensitivity;
//         transform.Rotate(0, mouseXRotation, 0);

//         verticalRotation -= lookInput.y * m_lookSensitivity;
//         verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
//         mainCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
//     }

//     // // Networking Staff

//     // [ServerRpc]
//     // private void JumpServerRPC()
//     // {
//     //     if (characterController.isGrounded)
//     //     {
//     //         currentMovement.y = jumpForce;
//     //         Debug.Log("Debug.Log(currentMovement.y): " + currentMovement.y);
//     //         Debug.Log("Jumping triggered.");
//     //     }
//     // }

//     // [ServerRpc]
//     // private void ShootServerRPC()
//     // {
//     //     weapon.Shooting();
//     //     Debug.Log("Shooting triggered.");
//     // }




}
