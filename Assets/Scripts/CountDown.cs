using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
using Goldmetal.UndeadSurvivor;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public AudioSource EscapeAudio;
    public AudioClip LoseAudio;
    public AudioClip WinAudio;

    public float totalTime = 240f; // �� �ð� (�� ����)
    private float currentTime;
    public Text timerText;
    private bool IsTimeOver;
    static public bool IsTimerRunning;       // �������� Ż���� ���������� Ÿ�̸Ӱ� ���߰� �ؾ� ��

    public GameObject VirusWinPanel;
    public GameObject PlayerLosePanel;

    private void Start()
    {
        EscapeAudio = GetComponent<AudioSource>();

        IsTimerRunning = true;

        // timerText = GetComponent<Text>();
        currentTime = totalTime;

        UpdateTimerText();
        InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f); // 1�ʸ��� Ÿ�̸Ӹ� ����
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
                // Ÿ�̸� ���� ó��
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