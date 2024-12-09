using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Trooper : RobotController
{
    // Dev Debug Staff
    [SerializeField] Canvas screenSpaceCanvas;

    [Header("Gameplay Variables")]
    private NetworkVariable<int> healthPoints = new NetworkVariable<int>();
    public int HealthPoints { get { return healthPoints.Value; } set { healthPoints.Value = value; } }
    
    private NetworkVariable<int> armorPoints = new NetworkVariable<int>();
    public int ArmorPoints { get { return armorPoints.Value; } set { armorPoints.Value = value; } }

    private NetworkVariable<int> ammoPoints = new NetworkVariable<int>();
    public int AmmoPoints { get { return ammoPoints.Value; } set { ammoPoints.Value = value; } }

    [SerializeField] private int initialHP = 100;
    public int InitialHP { get => initialHP; set => initialHP = value; }
    
    [SerializeField] private int initialArmor = 100;
    public int InitialArmor { get => initialArmor; set => initialArmor = value; }
    // Gameplay - Scene Awake & Start
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        audioListener = GetComponentInChildren<AudioListener>();
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
    void OnServerSpawnPlayer()
    {
        // this is done server side, so we have a single source of truth for our spawn point list
        var spawnPoint = ServerPlayerSpawnPoints.Instance.ConsumeNextSpawnPoint();
        var spawnPosition = spawnPoint ? spawnPoint.transform.position : Vector3.zero;
        transform.position = spawnPosition;
    }

    public override void OnNetworkSpawn()
    {
        OnServerSpawnPlayer();
        base.OnNetworkSpawn();

        playerInput.enabled = IsOwner;
        characterController.enabled = IsOwner;
        mainCam.enabled = IsOwner;
        audioListener.enabled = IsOwner;
        screenSpaceCanvas.enabled = IsOwner;

        // Game Variables
        healthPoints.Value = InitialHP;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} spawned with OwnerClientId: {OwnerClientId}");
        Debug.Log($"HP of OwnerClientID: {OwnerClientId} is {healthPoints.Value} when spawned.");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        playerInput.enabled = false;
        characterController.enabled = false;
        mainCam.enabled = false;
        audioListener.enabled = false;
        screenSpaceCanvas.enabled = false;

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
        weapon.Shooting();
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
