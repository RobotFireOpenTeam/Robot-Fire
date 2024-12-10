using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Trooper : RobotController
{
    // Dev Debug Staff
    [SerializeField] private Canvas _screenSpaceCanvas;

    [Header("Gameplay Variables")]
    public NetworkVariable<int> HealthPoints { get => _healthPoints; set => _healthPoints = value; }
    private NetworkVariable<int> _healthPoints = new NetworkVariable<int>();
    
    public NetworkVariable<int> ArmorPoints { get => _armorPoints; set => _armorPoints = value; }
    private NetworkVariable<int> _armorPoints = new NetworkVariable<int>();

    public NetworkVariable<int> AmmoPoints { get => _ammoPoints; set => _ammoPoints = value; }
    private NetworkVariable<int> _ammoPoints = new NetworkVariable<int>();

    public int InitialHP { get => _initialHP; set => _initialHP = value; }
    [SerializeField] private int _initialHP = 100;
    
    public int InitialArmor { get => _initialArmor; set => _initialArmor = value; }
    [SerializeField] private int _initialArmor = 100;

    // Gameplay - Scene Awake & Start
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _audioListener = GetComponentInChildren<AudioListener>();
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
    private void OnServerSpawnPlayer()
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

        _playerInput.enabled = IsOwner;
        _characterController.enabled = IsOwner;
        _mainCam.enabled = IsOwner;
        _audioListener.enabled = IsOwner;
        _screenSpaceCanvas.enabled = IsOwner;

        // Game Variables
        HealthPoints.Value = InitialHP;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} spawned with OwnerClientId: {OwnerClientId}");
        Debug.Log($"HP of OwnerClientID: {OwnerClientId} is {HealthPoints.Value} when spawned.");
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        _playerInput.enabled = false;
        _characterController.enabled = false;
        _mainCam.enabled = false;
        _audioListener.enabled = false;
        _screenSpaceCanvas.enabled = false;

        Debug.Log($"NetworkObject ID: {NetworkObjectId} despawned");
    }


    // Networking Staff - Gameplay
    
    [ServerRpc]
    private new void JumpServerRPC()
    {
        if (_characterController.isGrounded)
        {
            Debug.Log($"Jumping as Trooper triggered. Owner: {OwnerClientId}");
        }
    }

    [ServerRpc]
    private new void ShootServerRPC()
    {
        _weapon.Shooting();
    }

    [ServerRpc]
    private new void AimServerRPC()
    {
        Debug.Log($"Aim as Trooper triggered. Owner: {OwnerClientId}");
    }

    [ServerRpc]
    private new void CrouchServerRPC()
    {
        Debug.Log($"Crouch as Trooper triggered. Owner: {OwnerClientId}");
    }

    [ServerRpc]
    private new void InteractServerRPC()
    {
        Debug.Log($"Interact as Trooper triggered. Owner: {OwnerClientId}");
    }
}
