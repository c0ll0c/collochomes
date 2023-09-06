using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject PlayerWinPanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject PlayerLosePanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject VirusLosePanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��

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

        else if (PlayerLosePanel.activeSelf)                  // ���� Ż���� �� �ƴϰ� �� �±װ� �÷��̾�
        {
            //PlayerLosePanel.SetActive(true);
            EscapeAudio.clip = LoseAudio;
            EscapeAudio.Play();
        }
        
        else if (VirusLosePanel.activeSelf)                  // ���� Ż���� �� �ƴϰ� �� �±װ� ���̷���
        {
            //VirusLosePanel.SetActive(true);
            EscapeAudio.clip = LoseAudio;
            EscapeAudio.Play();
        }
       
    }
}
