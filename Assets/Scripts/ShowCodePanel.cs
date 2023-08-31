using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject GameOverObject;       // 보이게 할 단서 판넬을 할당해 줌

    public string correctCode;        // 정답 암호 설정
    public InputField inputField;            // InputField 연결

    private void Start()
    {
        GameOverObject = GameObject.Find("Canvas").gameObject;
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
            Debug.Log("정답입니다! 씬 전환 또는 다른 동작 수행");

            GameOverObject.SetActive(true);
            // 정답일 경우 GameOverScene으로 씬 전환
            //SceneManager.LoadScene("GameOverScene"); // "GameOverScene"은 실제로 사용하는 씬의 이름으로 변경해야 합니다.
        }
        else
        {
            Debug.Log("틀렸습니다. 다시 시도하세요.");
        }
    }
}
