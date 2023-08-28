using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 발견되는 순서에 따라 왼쪽부터 차례대로 바뀌는 것
// ShowCodeClue에서, isClicked가 한번 활성화될 때마다 count++;

public class CodeClue : MonoBehaviour
{
    static public int count = 0;           // 몇 번째로 발견된 단서인지, 

    public Text codeText_1;
    public Text codeText_2;
    public Text codeText_3;
    public Text codeText_4;
    public Text codeText_5;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(codeText_1);
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(UserName[0]);

        if (ShowCodeClue.code != null)         // a 단서가 이미 획득이 되었다면, 할당해 주기
        {
            if (count == 1)
                codeText_1.text = ShowCodeClue.code;
            if (count == 2)
                codeText_2.text = ShowCodeClue.code;
            if (count == 3)
                codeText_3.text = ShowCodeClue.code;
            if (count == 4)
                codeText_4.text = ShowCodeClue.code;
            if (count == 5)
                codeText_5.text = ShowCodeClue.code;
        }
    }
}
