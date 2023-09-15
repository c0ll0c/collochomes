using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ganerate Code Clue

public class GanerateClue : MonoBehaviour
{
    public List<Text> virusCodeTextList;
    public List<Text> playerNameTextList;
    public List<Text> playerCodeTextList;

    public static string virusCode;

    // Start is called before the first frame update
    void Start()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
            Debug.Log(currentPlayersStatus[i].PlayerStatus);

            if (playerNameTextList[i].text == "player" + i)
            {
                playerNameTextList[i].text = currentPlayersStatus[i].Nickname;
                playerCodeTextList[i].text = currentPlayersStatus[i].PlayerCode;
            }
        }
    }

    // Assign to Update because the virus is not set at the start of the game
    
    private void Update()
    {
        int index;

        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        for (int i = 0; i < currentPlayersStatus.Count; i++)                // �÷��̾� �Ѹ� �Ѹ� �����ͼ� �±װ� ���̷����� �÷��̾��� index ã��
        {
            if (currentPlayersStatus[i].PlayerStatus == "Virus")
            {
                index = i;

                virusCode = currentPlayersStatus[index].PlayerCode; // �÷��̾� �ڵ� ��ü ���ڿ� ��������

                //Debug.Log("���̷��� �ڵ�: " + virusCode);

                for (int j = 0; j < 5; j++)
                {
/*                    Debug.Log("�ݺ���");
                    Debug.Log(virusCode[j].ToString());*/

                    if (virusCodeTextList[j].text == "c" + j)
                    {
                        virusCodeTextList[j].text = virusCode[j].ToString();
                    }
                }

            }
        }
    }
}
