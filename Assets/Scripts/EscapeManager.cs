using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject PlayerWinPanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject VirusWinPanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject PlayerLosePanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject VirusLosePanel;       // 보이게 할 단서 판넬을 할당해 줌

    public AudioSource EscapeAudio;

    public AudioClip WinAudio;
    public AudioClip LoseAudio;

    bool IsGameOver;

    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
        // EscapeAudio = GetComponent<AudioSource>();
        // EscapeAudio.Play();

    }

    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (!IsGameOver)
        {
            if (PlayerWinPanel.activeSelf)
            {
                Debug.Log("플레이어 승!");
                //PlayerWinPanel.SetActive(true);
                EscapeAudio.clip = WinAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }

            if (VirusWinPanel.activeSelf)
            {
                Debug.Log("바이러스 승!");
                //PlayerWinPanel.SetActive(true);
                EscapeAudio.clip = WinAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }

            else if (PlayerLosePanel.activeSelf)
            {
                Debug.Log("플레이어 패");
                //PlayerLosePanel.SetActive(true);
                EscapeAudio.clip = LoseAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }

            else if (VirusLosePanel.activeSelf)
            {
                Debug.Log("바이러스 패");
                //VirusLosePanel.SetActive(true);
                EscapeAudio.clip = LoseAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }
        }

    }
}
