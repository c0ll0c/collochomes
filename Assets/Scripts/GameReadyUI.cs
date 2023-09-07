using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.UI;

public class GameReadyUI : MonoBehaviour
{
    public AudioSource audioSource;

    private List<PlayerData> playersInfo;
    private List<GameObject> playerPanel = new List<GameObject>(new GameObject[6]);
    private GameObject startButton;
    private GameObject exitButton;


    void Start()
    {
        GameObject panelParent = this.transform.Find("Player Info").gameObject;
        Debug.Log(panelParent);
        for (int i = 1; i <= 6; i++)
        {
            playerPanel[i-1] = panelParent.transform.Find("player" + i).gameObject;
            playerPanel[i-1].SetActive(false);
            Debug.Log(playerPanel[i-1]);
        }

        Debug.Log(PhotonNetwork.IsMasterClient);
        Debug.Log(PhotonNetwork.IsConnected);

        startButton = this.transform.Find("Start Button").gameObject;
        startButton.SetActive(false);

        exitButton = this.transform.Find("Exit Button").gameObject;
        exitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.InRoom)
        {
            if ((string)PhotonNetwork.CurrentRoom.CustomProperties["RoomState"] == "Playing")
            {
                Debug.Log("no");
                //NetworkManager.instance.ExitRoom();
            }
        }

        playersInfo = NetworkManager.instance.GetPlayersStatus();

        for (int i = 0; i < playersInfo.Count; i++)
        {
            playerPanel[i].transform.Find("text").gameObject.GetComponent<TextMeshProUGUI>().text = playersInfo[i].Nickname;
            playerPanel[i].SetActive(true);
        }

        for (int i = playersInfo.Count; i < 6; i++)
        {
            playerPanel[i].SetActive(false);
        }

        
        if (PhotonNetwork.IsMasterClient )
        {
            startButton.SetActive(true);
            exitButton.SetActive(false);
        }
        else
        {
            exitButton.SetActive(true);
            startButton.SetActive(false);
        }

    }

    public void StartButtonClicked()
    {
        audioSource.Play();
        StartCoroutine(WaitForSoundToStart());

    }
    private IEnumerator WaitForSoundToStart()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        if (playersInfo.Count > 0 && PhotonNetwork.IsMasterClient && (string)PhotonNetwork.CurrentRoom.CustomProperties["RoomState"] == "Waiting")
        {
            NetworkManager.instance.PlayerReady();
        }
    }

    public void ExitButtonClicked()
    {
        audioSource.Play();
        StartCoroutine(WaitForSoundToExit());

    }
    private IEnumerator WaitForSoundToExit()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        if (!PhotonNetwork.IsMasterClient && (string)PhotonNetwork.CurrentRoom.CustomProperties["RoomState"] == "Waiting")
        {
            NetworkManager.instance.ExitRoom();
        }
    }
}
