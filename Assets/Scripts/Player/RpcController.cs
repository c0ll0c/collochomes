using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Goldmetal.UndeadSurvivor;

public class RpcController : MonoBehaviour
{
    PlayerController player;
    public AudioClip InfectAudio;
    public AudioClip AttackAudio;
    public AudioSource audioSource;

    private void Start()
    {
        player = GetComponent<PlayerController>();   
    }

    [PunRPC]
    private void InfectRPC()    // �������ϴ� RPC + �������ϴ� ����� (22)
    {
        NetworkManager.instance.SetPlayerStatus("Infect");
        audioSource.clip = InfectAudio;
        audioSource.Play();
    }

    [PunRPC]
    private void AttackRPC()    // ���ݴ��ϴ� RPC (speed ����) + ���ݴ��ϴ� ����� (33)
    {
        NetworkManager.instance.SetPlayerSpeed(0);
        audioSource.clip = AttackAudio;
        audioSource.Play();
    }

    [PunRPC]
    private void ResetAttackRPC()   // ����Ǯ���� RPC (speed �ʱ�ȭ)
    {
        NetworkManager.instance.SetPlayerSpeed(3);
    }

}
