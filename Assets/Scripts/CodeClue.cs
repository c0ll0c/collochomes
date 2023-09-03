using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 단서 모음집에 암호 단서가 나타나는 것
// 발견되는 순서에 따라 왼쪽부터 차례대로 바뀌는 것
// ShowCodeClue에서, isClicked가 한번 활성화될 때마다 count++;

public class CodeClue : MonoBehaviour
{
    static public int count = 0;           // 몇 번째로 발견된 단서인지, 

    public List<Text> virusCodeTextList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(UserName[0]);

        if (ShowCodeClue.code != null)         // a 단서가 이미 획득이 되었다면, 할당해 주기
        {
            for (int i = 0; i < 5; i++)
            {
                if (count == i+1)
                    virusCodeTextList[i].text = ShowCodeClue.code;
            }
        }
    }
}
