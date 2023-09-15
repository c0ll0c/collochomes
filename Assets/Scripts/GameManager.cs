using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Goldmetal.UndeadSurvivor;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public bool isAlert = false;
    public GameObject gamePlayer;
    PlayerData myInfo;
    List<PlayerData> playersInfo = new List<PlayerData>();
    
    // UI
    [SerializeField] private GameObject[] detox;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject[] statusUI;
    [SerializeField] private GameObject[] gameUI;
    
    // spawn positions
    private Vector3[] randomPosition =
    {
        new Vector3(-9.0f, -6.0f, 0),
        new Vector3(-3.0f, 15.0f, 0),
        new Vector3(14.5f, 1.5f, 0),
        new Vector3(9.0f, 12.5f, 0),
        new Vector3(7.0f, 6.0f, 0),
        new Vector3(-2.0f, 0, 0),
    };
    private Vector3 myPos;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

        // Virus setting
        if (PhotonNetwork.CurrentRoom.CustomProperties["virusNo"] == null && PhotonNetwork.IsMasterClient)
        {
            List<int> playersNo = new List<int>();
            playersInfo = NetworkManager.instance.GetPlayersStatus();
            foreach (PlayerData p in playersInfo)
            {
                playersNo.Add(p.PlayerID);  // store player num
                myPos = randomPosition[playersNo.Count - 1];    // set spawn position
            }

            int virus = Random.Range(0, PhotonNetwork.CountOfPlayers);
            NetworkManager.instance.SetVirus(playersNo[virus]);
        }

        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        // 네트워크 유저에 각 플레이어 연결
        gamePlayer = PhotonNetwork.Instantiate("Player" + (myInfo.PlayerID % 4 + 1), myPos, Quaternion.identity) as GameObject;
        gamePlayer.GetComponent<PhotonView>().Owner.TagObject = gamePlayer;
    }


    void Start()
    {
        myInfo = NetworkManager.instance.GetMyStatus();
        playersInfo = NetworkManager.instance.GetPlayersStatus();

        // 기본 UI 및 아이템 설정
        foreach (GameObject d in detox)
        {
            d.SetActive(false);
        }
        loadingUI.SetActive(true);
        statusUI[0].SetActive(false);
        statusUI[1].SetActive(false);

        isAlert = true;     // loadingUI 중 isAlert = true
        CountDown.IsTimerRunning = false;
        StartCoroutine(setStatusUI());
    }


    void Update()
    {
        myInfo = NetworkManager.instance.GetMyStatus();

        // detox 활성화
        if (myInfo.PlayerStatus == "Infect")
        {
            foreach (GameObject d in detox)
            {
                d.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject d in detox)
            {
                d.SetActive(false);
            }
        }

        // player object와 연동 (isAlert, ending)
        if (gamePlayer == null) return;
        gamePlayer.GetComponent<PlayerController>().isAlert = isAlert; 
        if (gamePlayer.GetComponent<PlayerController>().ending)
        {
            isAlert = true;
            foreach (GameObject ui in gameUI)
            {
                ui.SetActive(false);
            }
        }

    }

    // player status에 따른 처음 등장 UI 설정
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
        isAlert = false;
        CountDown.IsTimerRunning = true;
    }

}