using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Goldmetal.UndeadSurvivor;

public class RpcController : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();   
    }

    [PunRPC]
    private void InfectRPC()    // 감염당하는 RPC
    {
        NetworkManager.instance.SetPlayerStatus("Infect");
    }

    [PunRPC]
    private void AttackRPC()    // 공격당하는 RPC (speed 감소)
    {
        NetworkManager.instance.SetPlayerSpeed(0);
    }

    [PunRPC]
    private void ResetAttackRPC()   // 공격풀리는 RPC (speed 초기화)
    {
        NetworkManager.instance.SetPlayerSpeed(3);
    }

}
