using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer;
using Goldmetal.UndeadSurvivor;

// Ż�� -> ���� �÷��̾�: �� / ���� �÷��̾� : ��
// ���� �÷��̾��� IsMineEscape = true, ���� �÷��̾��� IsOtherEscape = true�� �ؼ�... IsMineEscape�� true�̸� Win, IsOtherEscape�� true�̸� Lose
// ������ Ż���ߴ°� / Ż���� �ߴ°�

public class CodeInputField : MonoBehaviour
{
    public AudioSource EscapeAudio;

    public AudioClip WinAudio;

    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��

    public string correctCode;        // ���� ��ȣ ����
    public InputField inputField;            // InputField ����

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

    public void ShowPanel()                  // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        GameManager.instance.isAlert = true;
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()               // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        GameManager.instance.isAlert = false;
        CodePanelObject.SetActive(false);
        GameManager.instance.isAlert = false;
    }

    public void DoneButton()
    {
        string enteredCode = inputField.text;   // InputField�� �ؽ�Ʈ �� ��������

        Debug.Log("�Էµ� ��ȣ" + enteredCode);
        Debug.Log("����" + correctCode);

        if (enteredCode == correctCode)         // �Է��� �ڵ尡 ���� �ڵ�� ��ġ�ϴ��� Ȯ��
        {
            GameManager.instance.isAlert = true;
            SurvivorWinUI.SetActive(true);
            GameManager.instance.gamePlayer.GetComponent<PhotonView>().RPC("EndLoseUI", RpcTarget.OthersBuffered);
            GameManager.instance.gamePlayer.GetComponent<PlayerController>().ending = true;
            CodePanelObject.SetActive(false);
            Debug.Log("����! WIN");
            // �й� �����
            EscapeAudio.clip = WinAudio;
            EscapeAudio.Play();
            StartCoroutine(backIntro());
        }

        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�. �ٽ� �õ��ϼ���.");
            inputField.GetComponent<Animator>().SetTrigger("on");
        }

    }

    private IEnumerator backIntro()
    {
        yield return new WaitForSeconds(5.0f);
        NetworkManager.instance.ExitRoom();
    }
}