using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �ܼ� ����� -> �г����� �ϼ�, �ڵ�� �̿ϼ�
// �ڵ� ���� �˰��� ¥��, �ڵ� ����� �ּ� Ǯ��
// ������ 4���̶�� ġ��, 4���� ������Ʈ�� ���� ������ �־� �ֱ�

public class UserCluePanel : MonoBehaviour
{
    public List<Text> playerNameTextList;
    public List<Text> playerCodeTextList;

    // Start is called before the first frame update
    void Start()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
            if (playerNameTextList[i].text == "player" + i)
            {
                playerNameTextList[i].text = currentPlayersStatus[i].Nickname;
                playerCodeTextList[i].text = currentPlayersStatus[i].PlayerCode;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
