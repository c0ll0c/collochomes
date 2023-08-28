using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ܼ� ���� ��ư�� ������ CluePanel�� ���ü��� Ȱ��ȭ�� ��
// �ѹ� �ܼ� ���� ��ư�� ��������, "�̹� ȹ���� �ܼ�"��� �˸��� ���� �ϹǷ�, IsClicked ������ �������� �Ҵ�

public class ShowClue : MonoBehaviour
{
    public GameObject CluePanelObject;                  // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject AlreadyPanelObject;
    static public bool IsClicked = false;           // �������� �Ҵ�
    static public bool IsAlert = false;           // �������� �Ҵ�

    public void ShowPanel()                                // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        if (!IsClicked)                                 // �̹� ȹ���� �ܼ��� �ƴϸ�
        {
            IsClicked = true;                           // �̹� ȹ���� �ܼ����� �˷� ��
            CluePanelObject.SetActive(true);
        }

        else                                            // �̹� ȹ���� �ܼ���
        {
            if(!IsAlert)                                // �˶��� �� ���� ���� �� �۵��ϵ���
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
