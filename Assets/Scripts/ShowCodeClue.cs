using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ܼ� ���� ��ư�� ������ �� ��ȣ �ܼ��� ���̴� ��
// �ܼ� ȹ���� ����

public class ShowCodeClue : MonoBehaviour
{
    public GameObject CluePanelObject;                  // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;
    static public bool IsAlert = false;           // �������� �Ҵ�

    public Text Code;                         // ���� ���� �ִ� �ܼ�

    static public string code;

    public void ShowPanel()                                // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        if (!IsClicked)                                 // �̹� ȹ���� �ܼ��� �ƴϸ�
        {
            IsClicked = true;                           // �̹� ȹ���� �ܼ����� �˷� ��
            CluePanelObject.SetActive(true);
            CodeClue.count++;                           // �ѹ� �������� �����Ǹ�, �� ��°�� �߰��� �ܼ����� �˷� ��� ��
            code = Code.text;
        }

        else                                            // �̹� ȹ���� �ܼ���
        {
            if (!IsAlert)                                // �˶��� �� ���� ���� �� �۵��ϵ���
                StartCoroutine(DeactivateAlreadyPanel());
        }
    }

    IEnumerator DeactivateAlreadyPanel()
    {
        AlreadyPanelObject.SetActive(true);
        IsAlert = true;
        Debug.Log("�˶�" + IsAlert);

        yield return new WaitForSeconds(1f); // 2�� ���

        AlreadyPanelObject.SetActive(false);
        IsAlert = false;
        Debug.Log("�˶�" + IsAlert);
        // �̷��� �ִ� ���ȿ� ĳ���� �� �����̰� ���� (IsAlert == true)
    }

    public void HidePanel()                                // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        CluePanelObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
