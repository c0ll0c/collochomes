using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

// 탈출 -> 로컬 플레이어: 승 / 원격 플레이어 : 패
// 로컬 플레이어의 IsMineEscape = true, 원격 플레이어의 IsOtherEscape = true로 해서... IsMineEscape가 true이면 Win, IsOtherEscape가 true이면 Lose
// 본인이 탈출했는가 / 탈출을 했는가

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌

    public string correctCode;        // 정답 암호 설정
    public InputField inputField;            // InputField 연결

    static public bool IsMineEscape = false;
    static public bool IsEscape = false;

    private void Start()
    {
    }

    private void Update()
    {
        correctCode = CodeCluePanel.virusCode;
    }

    public void ShowPanel()                  // 단서 보기 버튼을 누르면 판넬이 보임
    {
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()               // 닫기 버튼을 누르면 판넬이 안 보임
    {
        CodePanelObject.SetActive(false);
    }

    public void DoneButton()
    {
        string enteredCode = inputField.text;   // InputField의 텍스트 값 가져오기

        Debug.Log("입력된 암호" + enteredCode);
        Debug.Log("정답" + correctCode);

        if (enteredCode == correctCode)         // 입력한 코드가 정답 코드와 일치하는지 확인
        {
            Debug.Log("정답! WIN");
            IsMineEscape = true;
            IsEscape = true;                            // 얘를 동기화시켜줘야 함...
        }

        else
        {
            Debug.Log("틀렸습니다. 다시 시도하세요.");
        }

    }
}
