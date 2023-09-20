using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer;
using Goldmetal.UndeadSurvivor;

// 탈출 -> 로컬 플레이어: 승 / 원격 플레이어 : 패
// 로컬 플레이어의 IsMineEscape = true, 원격 플레이어의 IsOtherEscape = true로 해서... IsMineEscape가 true이면 Win, IsOtherEscape가 true이면 Lose
// 본인이 탈출했는가 / 탈출을 했는가

public class CodeInputField : MonoBehaviour
{
    public AudioSource EscapeAudio;

    public AudioClip WinAudio;

    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌

    public string correctCode;        // 정답 암호 설정
    public InputField inputField;            // InputField 연결

    static public bool IsMineEscape = false;
    static public bool IsEscape = false;
    GameObject SurvivorWinUI;

    private void Start()
    {
        EscapeAudio = GetComponent<AudioSource>();

        SurvivorWinUI = GameObject.Find("game UI").transform.Find("PlayerWin").gameObject;
        SurvivorWinUI.SetActive(false);
    }

    private void Update()
    {
        correctCode = GanerateClue.virusCode;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Infect") && other.GetComponent<PhotonView>().IsMine)
        {
            CodePanelObject.SetActive(false);
            GameManager.instance.isAlert = false;
        }
    }

    public void ShowPanel()                  // 단서 보기 버튼을 누르면 판넬이 보임
    {
        GameManager.instance.isAlert = true;
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()               // 닫기 버튼을 누르면 판넬이 안 보임
    {
        GameManager.instance.isAlert = false;
        CodePanelObject.SetActive(false);
        GameManager.instance.isAlert = false;
    }

    public void DoneButton()
    {
        string enteredCode = inputField.text;   // InputField의 텍스트 값 가져오기

        Debug.Log("입력된 암호" + enteredCode);
        Debug.Log("정답" + correctCode);

        if (enteredCode == correctCode)         // 입력한 코드가 정답 코드와 일치하는지 확인
        {
            GameManager.instance.isAlert = true;
            SurvivorWinUI.SetActive(true);
            GameManager.instance.gamePlayer.GetComponent<PhotonView>().RPC("EndLoseUI", RpcTarget.OthersBuffered);
            GameManager.instance.gamePlayer.GetComponent<PlayerController>().ending = true;
            CodePanelObject.SetActive(false);
            Debug.Log("정답! WIN");
            // 패배 오디오
            EscapeAudio.clip = WinAudio;
            EscapeAudio.Play();
            StartCoroutine(backIntro());
        }

        else
        {
            Debug.Log("틀렸습니다. 다시 시도하세요.");
            inputField.GetComponent<Animator>().SetTrigger("on");
        }

    }

    private IEnumerator backIntro()
    {
        yield return new WaitForSeconds(5.0f);
        NetworkManager.instance.ExitRoom();
    }
}