using UnityEngine;

public class Weapon : MonoBehaviour
{
    // [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] protected Camera mainCam;

    public void Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
