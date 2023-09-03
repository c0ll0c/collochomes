using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;

public class CountDown : MonoBehaviour
{
    public float totalTime = 150f; // �� �ð� (�� ����)
    private float currentTime;
    private Text timerText;
    private bool IsTimeOver;
    static private bool IsTimerRunning;       // �������� Ż���� ���������� Ÿ�̸Ӱ� ���߰� �ؾ� ��

    public GameObject VirusWinPanel;
    public GameObject PlayerLosePanel;

    private void Start()
    {
        IsTimerRunning = true;

        timerText = GetComponent<Text>();
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
                    VirusWinPanel.SetActive(true);
                    
                }
                else
                {
                    GameManager.instance.isAlert = true;
                    PlayerLosePanel.SetActive(true);
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
}
