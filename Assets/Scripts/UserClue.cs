using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ܼ� �������� ���� �̸� �����ֱ�
// �����ڵ� �ܼ��� ȹ���ߴٸ�, �ش� ������ �´� �ڵ� �����ֱ�
// �ϼ�!

public class UserClue : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Text> playerNameTextList;
    public List<Text> playerCodeTextList;

    int index;

    void Start()                        // ���� ������ �ִ� �÷��̾��� �г����� �ܼ� ���������� ���� �ֱ�
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
            playerNameTextList[i].text = currentPlayersStatus[i].Nickname;
        }
    }

    private void Update()                           // ȹ���� �ܼ��� �г����� playerNameTextList�� �� ��° �ε������� Ȯ���ϰ�, �׿� �´� �ε����� �ڵ带 ������� �־� �ֱ�
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (ShowClue.username != null)         // ���� ȹ��� �ܼ��� UserName�� �� ��° �ε����� �����ϴ��� ã�ƾ� ��
        {
            for(int i = 0; i < currentPlayersStatus.Count; i++)
            {
                if (playerNameTextList[i].text == ShowClue.username)
                {
                    index = i;

                    for (int j = 0; j < currentPlayersStatus.Count; j++)
                    {
                        if (index == j)
                        {
                            playerCodeTextList[j].text = ShowClue.usercode;
                        }
                    }

                }
            }
        }
    }
}
