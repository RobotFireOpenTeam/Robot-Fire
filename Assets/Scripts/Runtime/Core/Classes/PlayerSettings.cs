using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerSettings : NetworkBehaviour
{
    public NetworkVariable<FixedString64Bytes> PlayerNickname;
}
