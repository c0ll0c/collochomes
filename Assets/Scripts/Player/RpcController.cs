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
    private void InfectRPC()    // �������ϴ� RPC
    {
        NetworkManager.instance.SetPlayerStatus("Infect");
    }

    [PunRPC]
    private void AttackRPC()    // ���ݴ��ϴ� RPC (speed ����)
    {
        NetworkManager.instance.SetPlayerSpeed(0);
    }

    [PunRPC]
    private void ResetAttackRPC()   // ����Ǯ���� RPC (speed �ʱ�ȭ)
    {
        NetworkManager.instance.SetPlayerSpeed(3);
    }

}
