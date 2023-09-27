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
    private void InfectRPC()    // �������ϴ� RPC + �������ϴ� ����� (22)
    {
        NetworkManager.instance.SetPlayerStatus("Infect");
        audioSource.clip = InfectAudio;
        audioSource.Play();

        // ȿ��
        GameObject effect = GameManager.instance.gamePlayer.transform.Find("effect").gameObject;
        Debug.Log(effect);
        effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[0];
        Debug.Log(effect.GetComponent<Animator>().runtimeAnimatorController);
        effect.SetActive(true);
        StartCoroutine(ResetEffect(effect));
    }

    [PunRPC]
    private void AttackRPC()    // ���ݴ��ϴ� RPC (speed ����) + ���ݴ��ϴ� ����� (33)
    {
        NetworkManager.instance.SetPlayerSpeed(0);
        audioSource.clip = AttackAudio;
        audioSource.Play();

        // ȿ��
        GameObject effect = GameManager.instance.gamePlayer.transform.Find("effect").gameObject;
        Debug.Log(effect);
        effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[1];
        Debug.Log(effect.GetComponent<Animator>().runtimeAnimatorController);
        effect.SetActive(true);
        StartCoroutine(ResetEffect(effect));
    }

    [PunRPC]
    private void ResetAttackRPC()   // ����Ǯ���� RPC (speed �ʱ�ȭ)
    {
        NetworkManager.instance.SetPlayerSpeed(3);
    }

    private IEnumerator ResetEffect(GameObject effect)      // ȿ�� �ִϸ��̼� �ʱ�ȭ
    {
        yield return new WaitForSeconds(1.0f);
        effect.SetActive(false);
    }

    [PunRPC]
    private void UnvaccineRPC()   // ��� ����
    {
        NetworkManager.instance.SetVaccinated(false);
    }
}
