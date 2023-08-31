using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 유저 단서 만들기 -> 닉네임은 완성, 코드는 미완성
// 코드 생성 알고리즘 짜고, 코드 만들고 주석 풀기
// 유저가 4명이라고 치면, 4개의 오브젝트를 만들어서 각각에 넣어 주기

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
