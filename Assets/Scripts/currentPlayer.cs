using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ܼ� �������� �ߴ� ���� �÷��̾� ���

public class currentPlayer : MonoBehaviour
{
    public List<GameObject> playerPanel;
    public List<GameObject> userClueObject;
    private int maxVisiblePlayers = 6; // �ִ� ǥ���ϰ� ���� �÷��̾� ��

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();
/*
        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
            playerPanel[i].SetActive(true);
        }*/

        for (int i = currentPlayersStatus.Count; i < maxVisiblePlayers; i++)
        {
            playerPanel[i].SetActive(false);
            userClueObject[i].SetActive(false);
        }

    }
}

// 1 true
// 2,3,4,5,6 false