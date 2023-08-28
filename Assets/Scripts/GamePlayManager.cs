using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject endingUI;
    public GameObject winUI;
    public GameObject loseUI;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 0.2f);
        endingUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }
    public void Spawn()
    {
        PlayerData myStatus = NetworkManager.instance.GetMyStatus();

        //PhotonNetwork.Instantiate("Pants Game Player Variant", new Vector3(0, 0, 0), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();
        PlayerData myStauts = NetworkManager.instance.GetMyStatus();
    }
}
