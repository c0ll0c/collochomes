using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    static UIManager s_instance;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject[] statusUI;
    [SerializeField] private GameObject[] gameUI;

    private GameObject infectIcon;
    private GameObject attackIcon;
    private GameObject clueIcon;
    GameObject VirusLoseUI;
    GameObject SurvivorLoseUI;

    public static UIManager instance
    {
        get
        {
            if (s_instance == null)
                s_instance = FindObjectOfType<UIManager>();
            return s_instance;
        }
    }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gamePlayer == null) return;
        if (GameManager.instance.gamePlayer.GetComponent<PlayerController>().ending) return;

        // player status�� ���� UI ����
        if (GameManager.instance.gamePlayer.tag == "Player")
        {
            infectIcon.SetActive(false);
            attackIcon.SetActive(true);
            clueIcon.SetActive(true);
        }
        else
        {
            attackIcon.SetActive(false);
            infectIcon.SetActive(true);
            clueIcon.SetActive(false);
        }
    }

    private void Init()
    {
        // �⺻ UI ����
        loadingUI.SetActive(true);
        statusUI[0].SetActive(false);
        statusUI[1].SetActive(false);
        CountDown.IsTimerRunning = false;
        StartCoroutine(setStatusUI());

        // UI ����
        infectIcon = GameObject.Find("game UI").transform.Find("infect").gameObject;
        attackIcon = GameObject.Find("game UI").transform.Find("attack").gameObject;
        clueIcon = GameObject.Find("game UI").transform.Find("clue").gameObject;

        // UI ����
        VirusLoseUI = GameObject.Find("game UI").transform.Find("VirusLose").gameObject;
        SurvivorLoseUI = GameObject.Find("game UI").transform.Find("PlayerLose").gameObject;
        VirusLoseUI.SetActive(false);
        SurvivorLoseUI.SetActive(false);
    }

    // player status�� ���� ó�� ���� UI ����
    private IEnumerator setStatusUI()
    {
        yield return new WaitForSeconds(1.0f);
        if (PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.CurrentRoom.CustomProperties["virusNo"])
        {
            NetworkManager.instance.SetPlayerStatus("Virus");
            statusUI[0].SetActive(true);
            loadingUI.SetActive(false);
        }
        else
        {
            NetworkManager.instance.SetPlayerStatus("Player");
            statusUI[1].SetActive(true);
            loadingUI.SetActive(false);
        }
        loadingUI.SetActive(false);
        StartCoroutine(closeStatusUI());
    }

    private IEnumerator closeStatusUI()
    {
        yield return new WaitForSeconds(3.0f);
        loadingUI.SetActive(false);
        statusUI[0].SetActive(false);
        statusUI[1].SetActive(false);
        GameManager.instance.isAlert = false;
        CountDown.IsTimerRunning = true;
    }

    public void closeAllUI()
    {
        foreach (GameObject ui in gameUI)
        {
            ui.SetActive(false);
        }
    }

    ///////

    public void startCoolTime()
    {
        infectIcon.GetComponent<CoolTime>().StartCoolTime();    // UI ������ ��Ÿ�� ����
        attackIcon.GetComponent<CoolTime>().StartCoolTime();    // UI ������ ��Ÿ�� ����
    }

    public void showLoseUI()
    {
        if ((string)NetworkManager.instance.GetMyStatus().PlayerStatus == "Virus")
        {
            VirusLoseUI.SetActive(true);
        }
        else
        {
            SurvivorLoseUI.SetActive(true);
        }
    }
}
