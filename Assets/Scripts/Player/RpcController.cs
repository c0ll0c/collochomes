using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RpcController : MonoBehaviour
{
    PlayerController player;
    public AudioClip InfectAudio;
    public AudioClip AttackAudio;
    public AudioSource audioSource;
    [SerializeField] RuntimeAnimatorController[] effectAni;

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

        // 효과
        GameObject effect = GameManager.instance.gamePlayer.transform.Find("effect").gameObject;
        Debug.Log(effect);
        effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[0];
        Debug.Log(effect.GetComponent<Animator>().runtimeAnimatorController);
        effect.SetActive(true);
        StartCoroutine(ResetEffect(effect));
    }

    [PunRPC]
    private void AttackRPC()    // 공격당하는 RPC (speed 감소) + 공격당하는 오디오 (33)
    {
        NetworkManager.instance.SetPlayerSpeed(0);
        audioSource.clip = AttackAudio;
        audioSource.Play();

        // 효과
        GameObject effect = GameManager.instance.gamePlayer.transform.Find("effect").gameObject;
        Debug.Log(effect);
        effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[1];
        Debug.Log(effect.GetComponent<Animator>().runtimeAnimatorController);
        effect.SetActive(true);
        StartCoroutine(ResetEffect(effect));
    }

    [PunRPC]
    private void ResetAttackRPC()   // 공격풀리는 RPC (speed 초기화)
    {
        NetworkManager.instance.SetPlayerSpeed(3);
    }

    private IEnumerator ResetEffect(GameObject effect)      // 효과 애니메이션 초기화
    {
        yield return new WaitForSeconds(1.0f);
        effect.SetActive(false);
    }

}
