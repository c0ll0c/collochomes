using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Goldmetal.UndeadSurvivor;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public bool isAlert = false;
    PlayerData myInfo;
    GameObject gamePlayer;
    List<PlayerData> playersInfo = new List<PlayerData>();
    [SerializeField] private GameObject[] detox;
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject[] statusUI;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

        if (PhotonNetwork.CurrentRoom.CustomProperties["virusNo"] == null && PhotonNetwork.IsMasterClient)
        {
            List<int> playersNo = new List<int>();
            playersInfo = NetworkManager.instance.GetPlayersStatus();
            foreach (PlayerData p in playersInfo)
            {
                playersNo.Add(p.PlayerID);
                Debug.Log(p.PlayerID + " " + playersNo.Count);
            }

            int virus = Random.Range(0, PhotonNetwork.CountOfPlayers);
            NetworkManager.instance.SetVirus(playersNo[virus]);
        }

        Invoke("Spawn", 0.2f);
    }

    void Start()
    {
        myInfo = NetworkManager.instance.GetMyStatus();
        playersInfo = NetworkManager.instance.GetPlayersStatus();

        foreach (GameObject d in detox)
        {
            d.SetActive(false);
        }

        loadingUI.SetActive(true);
        statusUI[0].SetActive(false);
        statusUI[1].SetActive(false);

        isAlert = true;
        StartCoroutine(setStatusUI());
    }

    public void Spawn()
    {
        gamePlayer = PhotonNetwork.Instantiate("Player" + (myInfo.PlayerID % 4 + 1), new Vector3(0 + myInfo.PlayerID, 0, 0), Quaternion.identity) as GameObject;
        gamePlayer.GetComponent<PhotonView>().Owner.TagObject = gamePlayer;

        /*
        Debug.Log("virusNo : " + (int)PhotonNetwork.CurrentRoom.CustomProperties["virusNo"]);
        if (PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.CurrentRoom.CustomProperties["virusNo"])
        {
            NetworkManager.instance.SetPlayerStatus("Virus");
        }
        else
        {
            NetworkManager.instance.SetPlayerStatus("Player");
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        myInfo = NetworkManager.instance.GetMyStatus();
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

        if (gamePlayer != null)
        {
            gamePlayer.GetComponent<PlayerController>().isAlert = isAlert;
        }
    }

    private IEnumerator setStatusUI()
    {
        yield return new WaitForSeconds(1.0f);
        if (PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.CurrentRoom.CustomProperties["virusNo"])
        {
            NetworkManager.instance.SetPlayerStatus("Virus");
            statusUI[0].SetActive(true);
        }
        else
        {
            NetworkManager.instance.SetPlayerStatus("Player");
            statusUI[1].SetActive(true);
        }
        loadingUI.SetActive(false);
        StartCoroutine(closeStatusUI());
    }

    private IEnumerator closeStatusUI()
    {
        yield return new WaitForSeconds(3.0f);
        loadingUI.SetActive(false );
        statusUI[0].SetActive(false);
        statusUI[1].SetActive(false);
        isAlert = false;
    }

}