using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ȣ �Է� ��ư�� ������ �� ��ȣ �Է� �ǳ��� �߰� �ϴ� ��ũ��Ʈ
// ShowClue.IsAlert�� GameManager�� ���� �� ��!

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;                  // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public string Code = "hi";

    public void ShowPanel()                                // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()                                // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        CodePanelObject.SetActive(false);
    }


    public void DoneButton()                        // inputField�� �Էµ� ����, ������ ����� ��ȣ�� ��ġ�ϸ� �� ��ȯ
    {

    }
    // Update is called once per frame

}

