using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �߰ߵǴ� ������ ���� ���ʺ��� ���ʴ�� �ٲ�� ��
// ShowCodeClue����, isClicked�� �ѹ� Ȱ��ȭ�� ������ count++;

public class CodeClue : MonoBehaviour
{
    static public int count = 0;           // �� ��°�� �߰ߵ� �ܼ�����, 

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

        if (ShowCodeClue.code != null)         // a �ܼ��� �̹� ȹ���� �Ǿ��ٸ�, �Ҵ��� �ֱ�
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
