using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ܼ� �������� ��ȣ �ܼ��� ��Ÿ���� ��
// �߰ߵǴ� ������ ���� ���ʺ��� ���ʴ�� �ٲ�� ��
// ShowCodeClue����, isClicked�� �ѹ� Ȱ��ȭ�� ������ count++;

public class CodeClue : MonoBehaviour
{
    static public int count = 0;           // �� ��°�� �߰ߵ� �ܼ�����, 

    public List<Text> virusCodeTextList;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(UserName[0]);

        if (ShowCodeClue.code != null)         // a �ܼ��� �̹� ȹ���� �Ǿ��ٸ�, �Ҵ��� �ֱ�
        {
            for (int i = 0; i < 5; i++)
            {
                if (count == i+1)
                    virusCodeTextList[i].text = ShowCodeClue.code;
            }
        }
    }
}
