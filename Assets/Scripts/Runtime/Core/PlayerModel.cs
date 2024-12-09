using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerModel : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // Исключаем слой ModelLocal из рендеринга для mainCamera
        if (IsLocalPlayer)
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer("ModelLocal"));
        }
        else
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer("ModelRemote"));
        }
        Debug.Log($"NetworkObject ID: {NetworkObjectId} RobotModel's LayerMask: " + gameObject.layer);
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child == null)
            {
                continue;
            }

            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
