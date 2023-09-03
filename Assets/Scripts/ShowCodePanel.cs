using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌
    public string correctCode = "hi";        // 정답 암호 설정

    public InputField inputField;            // InputField 연결

    public void ShowPanel()                  // 단서 보기 버튼을 누르면 판넬이 보임
    {
        GameManager.instance.isAlert = true;
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()               // 닫기 버튼을 누르면 판넬이 안 보임
    {
        GameManager.instance.isAlert = false;
        CodePanelObject.SetActive(false);
        
    }

    public void DoneButton()
    {
        string enteredCode = inputField.text;   // InputField의 텍스트 값 가져오기

        if (enteredCode == correctCode)         // 입력한 코드가 정답 코드와 일치하는지 확인
        {
            Debug.Log("정답입니다! 씬 전환 또는 다른 동작 수행");
        }
        else
        {
            Debug.Log("틀렸습니다. 다시 시도하세요.");
        }
    }
}
