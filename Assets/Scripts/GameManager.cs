using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public bool isAlert = false;
    PlayerData myInfo;
    List<PlayerData> playersInfo = new List<PlayerData>();
    List<bool> hasClue;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;

        if (PhotonNetwork.CurrentRoom.CustomProperties["virusNo"] == null && PhotonNetwork.IsMasterClient)
        {

            int virus = Random.Range(1, PhotonNetwork.CountOfPlayers + 1);
            NetworkManager.instance.SetVirus(virus);
        }

        Invoke("Spawn", 0.2f);
    }

    void Start()
    {
        myInfo = NetworkManager.instance.GetMyStatus();
        playersInfo = NetworkManager.instance.GetPlayersStatus();
    }
    public void Spawn()
    {
        GameObject gamePlayer = PhotonNetwork.Instantiate("Player" + myInfo.PlayerID, new Vector3(0 + myInfo.PlayerID, 0, 0), Quaternion.identity) as GameObject;
        gamePlayer.GetComponent<PhotonView>().Owner.TagObject = gamePlayer;

        Debug.Log("virusNo : " + (int)PhotonNetwork.CurrentRoom.CustomProperties["virusNo"]);
        if (PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.CurrentRoom.CustomProperties["virusNo"])
        {
            NetworkManager.instance.SetPlayerStatus("Virus");
        }
        else
        {
            NetworkManager.instance.SetPlayerStatus("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerData myStatus = NetworkManager.instance.GetMyStatus();
    }

    public bool getIsAlert()
    {
        return isAlert;
    }
    public void setIsAlert()
    {
        isAlert = !isAlert;
    }
}
