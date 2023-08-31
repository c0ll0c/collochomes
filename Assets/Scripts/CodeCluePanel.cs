using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 암호 단서를 플레이어와 연동해서 만들어 주는 스크립트
// 바이러스 태그를 가진 플레이어의 code를 가져와서 하나하나 넣어 주기
// 완료

public class CodeCluePanel : MonoBehaviour
{
    public List<Text> virusCodeTextList;
    public static string virusCode;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("바이러스 찾기");

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

        for (int i = 0; i < currentPlayersStatus.Count; i++)                // 플레이어 한명 한명 데려와서 태그가 바이러스인 플레이어의 index 찾기
        {
            if (currentPlayersStatus[i].PlayerStatus == "Virus")
            {
                index = i;

                virusCode = currentPlayersStatus[index].PlayerCode; // 플레이어 코드 전체 문자열 가져오기

                //Debug.Log("바이러스 코드: " + virusCode);

                for (int j = 0; j < 5; j++)
                {
/*                    Debug.Log("반복문");
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
