using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject PlayerWinPanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject PlayerLosePanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject VirusLosePanel;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (ShowCodePanel.IsMineEscape)
        {
            PlayerWinPanel.SetActive(true);
        }
        else if (ShowCodePanel.IsEscape && currentPlayersStatus[0].PlayerStatus != "Virus")                  // ���� Ż���� �� �ƴϰ� �� �±װ� �÷��̾�
        {
            PlayerLosePanel.SetActive(true);
        }
        else if (ShowCodePanel.IsEscape && currentPlayersStatus[0].PlayerStatus == "Virus")                  // ���� Ż���� �� �ƴϰ� �� �±װ� ���̷���
        {
            VirusLosePanel.SetActive(true);
        }
    }
}
