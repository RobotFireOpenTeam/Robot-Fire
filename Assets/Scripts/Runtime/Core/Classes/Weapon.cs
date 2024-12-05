using UnityEngine;

public class Weapon : MonoBehaviour
{
    // [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] protected Camera mainCam;
    [SerializeField] protected float debugRayDistance = 5f;
    [SerializeField] protected float debugRayDuration = 2f;

    public void Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            Vector3 rayDir = mainCam.transform.forward * debugRayDistance;
            Debug.DrawRay(mainCam.transform.position, rayDir, Color.yellow, debugRayDuration, false);
        }
    }

    // Debug Shooting
    
}
