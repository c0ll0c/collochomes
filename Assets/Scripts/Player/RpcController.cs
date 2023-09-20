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
    private void InfectRPC()    // 감염당하는 RPC + 감염당하는 오디오 (22)
    {
        NetworkManager.instance.SetPlayerStatus("Infect");
        audioSource.clip = InfectAudio;
        audioSource.Play();
    }

    [PunRPC]
    private void AttackRPC()    // 공격당하는 RPC (speed 감소) + 공격당하는 오디오 (33)
    {
        NetworkManager.instance.SetPlayerSpeed(0);
        audioSource.clip = AttackAudio;
        audioSource.Play();
    }

    [PunRPC]
    private void ResetAttackRPC()   // 공격풀리는 RPC (speed 초기화)
    {
        NetworkManager.instance.SetPlayerSpeed(3);
    }

}
