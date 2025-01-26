using UnityEngine;
using Unity.Netcode;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Camera _mainCam;
    [SerializeField] protected float _debugRayDistance = 5f;
    [SerializeField] protected float _debugRayDuration = 2f;
    [SerializeField] private float _range = 100f;

    [Header("Gameplay Variables")]
    public NetworkVariable<float> Damage { get => _damage; set => _damage = value; }
    private NetworkVariable<float> _damage = new NetworkVariable<float>();

    public void Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(_mainCam.transform.position, _mainCam.transform.forward, out hit, _range))
        {
            Debug.Log(hit.transform.name);
            
            Vector3 rayDir = _mainCam.transform.forward * _debugRayDistance;
            Debug.DrawRay(_mainCam.transform.position, rayDir, Color.yellow, _debugRayDuration, false);
        }
    }

    // Debug Shooting
    
}
