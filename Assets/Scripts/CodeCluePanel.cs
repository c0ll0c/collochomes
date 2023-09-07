using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ȣ �ܼ��� �÷��̾�� �����ؼ� ����� �ִ� ��ũ��Ʈ
// ���̷��� �±׸� ���� �÷��̾��� code�� �����ͼ� �ϳ��ϳ� �־� �ֱ�
// �Ϸ�

public class CodeCluePanel : MonoBehaviour
{
    public List<Text> virusCodeTextList;
    public static string virusCode;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("���̷��� ã��");

        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        /////////////
        for(int p = 0; p < currentPlayersStatus.Count; p++)
        {
            Debug.Log(currentPlayersStatus[p].PlayerStatus);
        }
        /////////////
    }

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
