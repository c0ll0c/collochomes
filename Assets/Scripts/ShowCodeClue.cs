using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 단서 보기 버튼을 눌렀을 때 암호 단서가 보이는 것
// 단서 획득을 감지

public class ShowCodeClue : MonoBehaviour
{
    public GameObject CluePanelObject;                  // 보이게 할 단서 판넬을 할당해 줌
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;
    static public bool IsAlert = false;           // 전역으로 할당

    public Text Code;                         // 지금 보고 있는 단서

    static public string code;

    public void ShowPanel()                                // 단서 보기 버튼을 누르면 판넬이 보임
    {
        if (!IsClicked)                                 // 이미 획득한 단서가 아니면
        {
            IsClicked = true;                           // 이미 획득한 단서임을 알려 줌
            CluePanelObject.SetActive(true);
            CodeClue.count++;                           // 한번 눌렸음이 감지되면, 몇 번째로 발견한 단서인지 알려 줘야 함
            code = Code.text;
        }

        else                                            // 이미 획득한 단서면
        {
            if (!IsAlert)                                // 알람이 떠 있지 않을 때 작동하도록
                StartCoroutine(DeactivateAlreadyPanel());
        }
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
        CluePanelObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
