using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GameManager.instance.isAlert = true;
            CluePanelObject.SetActive(true);
            
            CodeClue.count++;                           // �ѹ� �������� �����Ǹ�, �� ��°�� �߰��� �ܼ����� �˷� ��� ��
                code = Code.text;
}

        else                                            // �̹� ȹ���� �ܼ���
        {
            if (!GameManager.instance.isAlert)                                // �˶��� �� ���� ���� �� �۵��ϵ���
                StartCoroutine(DeactivateAlreadyPanel());
        }
    }

    IEnumerator DeactivateAlreadyPanel()
    {
        AlreadyPanelObject.SetActive(true);
        GameManager.instance.isAlert = true;
        Debug.Log("�˶�" + IsAlert);

        yield return new WaitForSeconds(1f); // 2�� ���

        GameManager.instance.isAlert = false;
        AlreadyPanelObject.SetActive(false);
        Debug.Log("�˶�" + IsAlert);
        // �̷��� �ִ� ���ȿ� ĳ���� �� �����̰� ���� (IsAlert == true)
    }

    public void HidePanel()                                // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        GameManager.instance.isAlert = false;
        CluePanelObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
