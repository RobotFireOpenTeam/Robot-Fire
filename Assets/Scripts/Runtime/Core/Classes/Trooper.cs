using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Trooper : RobotController
{
    // Gameplay - Scene Awake & Start
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    // PlayerInput Events
    // overrided Inputs Here

    // Gameplay - Game Loop

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

    // Networking Staff - Spawn & Despawn

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        playerInput.enabled = IsOwner;
        characterController.enabled = IsOwner;
        mainCam.enabled = IsOwner;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} spawned with OwnerClientId: {OwnerClientId}");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        playerInput.enabled = false;
        characterController.enabled = false;
        mainCam.enabled = false;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} despawned");
    }


    // Networking Staff - Gameplay
    
    [ServerRpc]
    private new void JumpServerRPC()
    {
        if (characterController.isGrounded)
        {
            Debug.Log("Jumping as Trooper triggered.");
        }
    }

    [ServerRpc]
    private new void ShootServerRPC()
    {
        Debug.Log("Shooting as Trooper triggered.");
    }

    [ServerRpc]
    private new void AimServerRPC()
    {
        Debug.Log("Aim as Trooper triggered.");
    }

    [ServerRpc]
    private new void CrouchServerRPC()
    {
        Debug.Log("Crouch as Trooper triggered.");
    }

    [ServerRpc]
    private new void InteractServerRPC()
    {
        Debug.Log("Interact as Trooper triggered.");
    }
}
