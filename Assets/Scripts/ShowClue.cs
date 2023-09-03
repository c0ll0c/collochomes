using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 단서 보기 버튼을 누르면 CluePanel의 가시성이 활성화가 됨
// 한번 단서 보기 버튼을 눌렀으면, "이미 획득한 단서"라는 알림이 떠야 함

public class ShowClue : MonoBehaviour
{
    public AudioSource doneAudio;

    public GameObject CluePanelObject;                  // 보이게 할 단서 판넬을 할당해 줌
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;
    static public bool IsAlert = false;           // 전역으로 할당

    public Text UserName;                         // 지금 보고 있는 단서의 유저 이름
    public Text UserCode;                         // 지금 보고 있는 단서의 유저 코드

    static public string username;
    static public string usercode;

    public void ShowPanel()                                // 단서 보기 버튼을 누르면 판넬이 보임
    {
        if (!IsClicked)                                 // 이미 획득한 단서가 아니면
        {
            IsClicked = true;                           // 이미 획득한 단서임을 알려 줌
            CluePanelObject.SetActive(true);
        }

        else                                            // 이미 획득한 단서면
        {
            if(!IsAlert)                                // 알람이 떠 있지 않을 때 작동하도록
                StartCoroutine(DeactivateAlreadyPanel());
        }
        username = UserName.text;                       // Text 컴포넌트에 있는 텍스트를 스크립트에서 사용할 수 있도록 변수에 할당, 단서 모음집 스크립트에서 사용, 
        usercode = UserCode.text;
    }

    IEnumerator DeactivateAlreadyPanel()
    {
        AlreadyPanelObject.SetActive(true);
        IsAlert = true;
        Debug.Log("알람" + IsAlert);

        yield return new WaitForSeconds(1f); // 2초 대기

        AlreadyPanelObject.SetActive(false);
        IsAlert = false;
        Debug.Log("알람" + IsAlert);
        // 이러고 있는 동안에 캐릭터 못 움직이게 해줘 (IsAlert == true)
    }

    public void HidePanel()                                // 닫기 버튼을 누르면 판넬이 안 보임
    {
        doneAudio.Play();
        StartCoroutine(WaitForSoundToDone());

    }

    private IEnumerator WaitForSoundToDone()
    {
        yield return new WaitForSeconds(doneAudio.clip.length);

        CluePanelObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
