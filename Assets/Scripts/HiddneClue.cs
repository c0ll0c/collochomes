using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// �ݷ��� �ܼ� ����� ��ư�� ������ �ܼ��� ������ -> Ŭ�� ��, IsHidden = true
// ������ �ܼ�! �ǳ��� 1�� ���ٰ� �����
// 15�� �� �ܼ��� �������ٰ� �ٽ� ��
// ������ �ܼ��鿡 �پ� �־�� ��

public class HiddneClue : MonoBehaviour
{
    static public bool IsHidden;
    public GameObject HiddenCluePanel;
    float hiddenTime = 15.0f;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            IsHidden = false;
        }
    }

    public void hiddenButtonClick()
    {
        if (photonView.IsMine)
        {
            IsHidden = true;
            HiddenCluePanel.SetActive(true);                // ������ �ܼ�! ������
            GameManager.instance.isAlert = true;
            StartCoroutine(DeactivateHiddenCluePanel());    // ������ �ܼ�! ������
            StartCoroutine(UnHiddenClue());

            photonView.RPC("SyncIsHidden", RpcTarget.Others, true);
/*
            HiddenCluePanel.SetActive(true);
            GameManager.instance.isAlert = true;
            StartCoroutine(DeactivateHiddenCluePanel());
            StartCoroutine(UnHiddenClue());*/
        }
    }

    [PunRPC]
    void SyncIsHidden(bool isHidden)
    {
        IsHidden = isHidden;
        StartCoroutine(UnHiddenClue());
    }

    IEnumerator DeactivateHiddenCluePanel()
    {
        yield return new WaitForSeconds(1f);
        HiddenCluePanel.SetActive(false);
        GameManager.instance.isAlert = false;
    }

    IEnumerator UnHiddenClue()
    {
        yield return new WaitForSeconds(hiddenTime);
        IsHidden = false;
    }

}
