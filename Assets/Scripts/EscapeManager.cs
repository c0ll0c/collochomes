using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeManager : MonoBehaviour
{
    public GameObject CodePanelObject;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject PlayerWinPanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject PlayerLosePanel;       // 보이게 할 단서 판넬을 할당해 줌
    public GameObject VirusLosePanel;       // 보이게 할 단서 판넬을 할당해 줌

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
        else if (ShowCodePanel.IsEscape && currentPlayersStatus[0].PlayerStatus != "Virus")                  // 내가 탈출한 게 아니고 내 태그가 플레이어
        {
            PlayerLosePanel.SetActive(true);
        }
        else if (ShowCodePanel.IsEscape && currentPlayersStatus[0].PlayerStatus == "Virus")                  // 내가 탈출한 게 아니고 내 태그가 바이러스
        {
            VirusLosePanel.SetActive(true);
        }
    }
}
