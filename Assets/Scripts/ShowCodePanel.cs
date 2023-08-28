using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 암호 입력 버튼을 눌렀을 때 암호 입력 판넬이 뜨게 하는 스크립트
// ShowClue.IsAlert는 GameManager로 따로 뺄 것!

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;                  // 보이게 할 단서 판넬을 할당해 줌
    public string Code = "hi";

    public void ShowPanel()                                // 단서 보기 버튼을 누르면 판넬이 보임
    {
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()                                // 닫기 버튼을 누르면 판넬이 안 보임
    {
        CodePanelObject.SetActive(false);
    }


    public void DoneButton()                        // inputField에 입력된 값과, 서버에 저장된 암호가 일치하면 씬 전환
    {

    }
    // Update is called once per frame

}

