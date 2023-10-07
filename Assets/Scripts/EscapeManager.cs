using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject PlayerWinPanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject VirusWinPanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject PlayerLosePanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject VirusLosePanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��

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
                Debug.Log("�÷��̾� ��!");
                //PlayerWinPanel.SetActive(true);
                EscapeAudio.clip = WinAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }

            if (VirusWinPanel.activeSelf)
            {
                Debug.Log("���̷��� ��!");
                //PlayerWinPanel.SetActive(true);
                EscapeAudio.clip = WinAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }

            else if (PlayerLosePanel.activeSelf)
            {
                Debug.Log("�÷��̾� ��");
                //PlayerLosePanel.SetActive(true);
                EscapeAudio.clip = LoseAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }

            else if (VirusLosePanel.activeSelf)
            {
                Debug.Log("���̷��� ��");
                //VirusLosePanel.SetActive(true);
                EscapeAudio.clip = LoseAudio;
                EscapeAudio.Play();
                IsGameOver = true;
            }
        }

    }
}
