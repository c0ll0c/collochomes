using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 단서 모음집에 유저 이름 보여주기
// 유저코드 단서를 획득했다면, 해당 유저에 맞는 코드 보여주기
// 완성!

public class GotClue : MonoBehaviour
{
    // Start is called before the first frame update

    static public int count = 0;           // 몇 번째로 발견된 단서인지, 

    public List<Text> virusCodeTextList;

    public List<Text> playerNameTextList;
    public List<Text> playerCodeTextList;

    int index;

    void Start()                        // 현재 접속해 있는 플레이어의 닉네임을 단서 모음집에서 보여 주기
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
            playerNameTextList[i].text = currentPlayersStatus[i].Nickname;
        }
    }

    private void Update()                           // 획득한 단서의 닉네임이 playerNameTextList의 몇 번째 인덱스인지 확인하고, 그에 맞는 인덱스의 코드를 순서대로 넣어 주기
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (UserClueActive.username != null)         // 지금 획득된 단서의 UserName이 몇 번째 인덱스에 존재하는지 찾아야 함
        {
            for(int i = 0; i < currentPlayersStatus.Count; i++)
            {
                if (playerNameTextList[i].text == UserClueActive.username)
                {
                    index = i;

                    for (int j = 0; j < currentPlayersStatus.Count; j++)
                    {
                        if (index == j)
                        {
                            playerCodeTextList[j].text = UserClueActive.usercode;
                        }
                    }

                }
            }
        }

        if (CodeClueActive.code != null)         // a 단서가 이미 획득이 되었다면, 할당해 주기
        {
            for (int i = 0; i < 5; i++)
            {
                if (count == i + 1)
                    virusCodeTextList[i].text = CodeClueActive.code;
            }
        }
    }
}
