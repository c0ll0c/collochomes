using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager s_instance;
    public bool isAlert = false;
    public GameObject gamePlayer;
    PlayerData myInfo;
    List<PlayerData> playersInfo = new List<PlayerData>();
    List<int> playersNo = new List<int>();

    [SerializeField] private GameObject[] detox;

    public static GameManager instance
    {
        get
        {
            if (s_instance == null)
                s_instance = FindObjectOfType<GameManager>();
            return s_instance;
        }
    }

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
        s_instance = this;
        Application.targetFrameRate = 60;
        
        playersInfo = NetworkManager.instance.GetPlayersStatus();
        foreach (PlayerData p in playersInfo)
        {
            playersNo.Add(p.PlayerID);  // store player num
        }
        myPos = randomPosition[playersNo.Count - 1];    // set spawn position
        

        // Virus setting
        if (PhotonNetwork.CurrentRoom.CustomProperties["virusNo"] == null && PhotonNetwork.IsMasterClient)
        {
            int virus = Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount);
            NetworkManager.instance.SetVirus(playersNo[virus]);
        }

        Invoke("Spawn", 0.2f);
    }

    public void Spawn()
    {
        // spawn position
        playersNo.Sort();
        myPos = randomPosition[playersNo.IndexOf(myInfo.PlayerID)];

        // 네트워크 유저에 각 플레이어 연결
        gamePlayer = PhotonNetwork.Instantiate("Homes " + (playersNo.IndexOf(myInfo.PlayerID) + 1), myPos, Quaternion.identity) as GameObject;
        gamePlayer.GetComponent<PhotonView>().Owner.TagObject = gamePlayer;
    }


    void Start()
    {
        myInfo = NetworkManager.instance.GetMyStatus();
        playersInfo = NetworkManager.instance.GetPlayersStatus();
        isAlert = true;     // loadingUI 중 isAlert = true
        
    }


    void Update()
    {
        myInfo = NetworkManager.instance.GetMyStatus();

        // player object와 연동 (isAlert, ending)
        if (gamePlayer == null) return;
        gamePlayer.GetComponent<PlayerController>().isAlert = isAlert; 

        if (gamePlayer.GetComponent<PlayerController>().ending)
        {
            isAlert = true;
            UIManager.instance.closeAllUI();
        }
    }
}