using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public AudioSource EscapeAudio;
    public AudioClip LoseAudio;
    public AudioClip WinAudio;

    public float totalTime = 240f; // 총 시간 (초 단위)
    private float currentTime;
    public Text timerText;
    private bool IsTimeOver;
    static public bool IsTimerRunning;       // 누군가가 탈출을 성공했으면 타이머가 멈추게 해야 함

    public GameObject VirusWinPanel;
    public GameObject PlayerLosePanel;

    private void Start()
    {
        EscapeAudio = GetComponent<AudioSource>();

        IsTimerRunning = true;

        // timerText = GetComponent<Text>();
        currentTime = totalTime;

        UpdateTimerText();
        InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f); // 1초마다 타이머를 갱신
    }

    private void UpdateTimer()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (IsTimerRunning)
        {
            if (currentTime > 0)
            {
                currentTime--;
                UpdateTimerText();
            }
            else
            {
                // 타이머 종료 처리
                CancelInvoke(nameof(UpdateTimer));
                timerText.text = "Time's up!";

                if (currentPlayersStatus[0].PlayerStatus == "Virus")
                {
                    GameManager.instance.isAlert = true;
                    GameManager.instance.gamePlayer.GetComponent<PlayerController>().ending = true;
                    VirusWinPanel.SetActive(true);
                    EscapeAudio.clip = WinAudio;
                    EscapeAudio.Play();
                    StartCoroutine(backIntro());
                }
                else
                {
                    GameManager.instance.isAlert = true;
                    GameManager.instance.gamePlayer.GetComponent<PlayerController>().ending = true;
                    PlayerLosePanel.SetActive(true);
                    EscapeAudio.clip = LoseAudio;
                    EscapeAudio.Play();
                    StartCoroutine(backIntro());
                }

                IsTimerRunning = false;
            }
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator backIntro()
    {
        yield return new WaitForSeconds(5.0f);
        NetworkManager.instance.ExitRoom();
    }
}