using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// 콜록이 단서 숨기기 버튼을 누르면 단서가 숨겨짐 -> 클릭 시, IsHidden = true
// 숨겨진 단서! 판넬이 1초 떴다가 사라짐
// 15초 간 단서가 숨겨졌다가 다시 뜸
// 각각의 단서들에 붙어 있어야 함

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
            HiddenCluePanel.SetActive(true);                // 숨겨진 단서! 켜지기
            GameManager.instance.isAlert = true;
            StartCoroutine(DeactivateHiddenCluePanel());    // 숨겨진 단서! 꺼지기
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
