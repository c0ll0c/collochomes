using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject PlayerWinPanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject PlayerLosePanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject VirusLosePanel;       // 보이게 할 단서 판넬을 할당해 줌

    public AudioSource EscapeAudio;

    public AudioClip WinAudio;
    public AudioClip LoseAudio;

    // Start is called before the first frame update
    void Start()
    {
        // EscapeAudio = GetComponent<AudioSource>();
        // EscapeAudio.Play();

    }

    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (PlayerWinPanel.activeSelf)
        {
            //PlayerWinPanel.SetActive(true);
            EscapeAudio.clip = WinAudio;
            EscapeAudio.Play();
        }

        else if (PlayerLosePanel.activeSelf)                  // 내가 탈출한 게 아니고 내 태그가 플레이어
        {
            //PlayerLosePanel.SetActive(true);
            EscapeAudio.clip = LoseAudio;
            EscapeAudio.Play();
        }
        
        else if (VirusLosePanel.activeSelf)                  // 내가 탈출한 게 아니고 내 태그가 바이러스
        {
            //VirusLosePanel.SetActive(true);
            EscapeAudio.clip = LoseAudio;
            EscapeAudio.Play();
        }
       
    }
}
