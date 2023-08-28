using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 쪽지처럼 생긴 버튼 누르면 단서 모음집 창이 뜨게 하는 스크립트
// 쪽지 버튼에 할당해 주기

public class ShowClueCollection : MonoBehaviour
{
    public GameObject button;       // 쪽지처럼 생긴 버튼
    public GameObject CollectionPanel;        // 단서 모음집 판넬

    // Start is called before the first frame update
    void Start()
    {
        CollectionPanel.SetActive(false);
    }

    public void ShowCollection ()
    {
        CollectionPanel.SetActive(true);            // 버튼 눌리면 단서 모음집 판넬 가시성이 보이게 하기
        // isAlert = true로 해서 캐릭터 못 움직이게 할 것
    }

    public void CloseCollection() {
        CollectionPanel.SetActive(false);
        // isAlert = false로 해서 캐릭터 움직일 수 있게
    }

}
