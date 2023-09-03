using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �ܼ� ���� ��ư�� ������ CluePanel�� ���ü��� Ȱ��ȭ�� ��
// �ѹ� �ܼ� ���� ��ư�� ��������, "�̹� ȹ���� �ܼ�"��� �˸��� ���� ��
// 
public class ShowClue : MonoBehaviour
{
    public GameObject CluePanelObject;                  // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;
    public Text UserName;                         // ���� ���� �ִ� �ܼ��� ���� �̸�
    public Text UserCode;                         // ���� ���� �ִ� �ܼ��� ���� �ڵ�

    static public string username;
    static public string usercode;
/*
    private void Start()
    {
        Debug.Log(UserName_a.text);
        Debug.Log(UserCode_a.text);

        username = UserName_a.text;
        usercode = UserCode_a.text;
    }*/

    public void ShowPanel()                                // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        if (!IsClicked)                                 // �̹� ȹ���� �ܼ��� �ƴϸ�
        {
            IsClicked = true;                           // �̹� ȹ���� �ܼ����� �˷� ��
            GameManager.instance.isAlert = true;
            CluePanelObject.SetActive(true);
        }

        else                                            // �̹� ȹ���� �ܼ���
        {
            if(!GameManager.instance.isAlert)                                // �˶��� �� ���� ���� �� �۵��ϵ���
                StartCoroutine(DeactivateAlreadyPanel());
        }
        username = UserName.text;
        usercode = UserCode.text;
    }

    IEnumerator DeactivateAlreadyPanel()
    {
        AlreadyPanelObject.SetActive(true);
        GameManager.instance.isAlert = true;

        yield return new WaitForSeconds(1f); // 2�� ���

        AlreadyPanelObject.SetActive(false);
        GameManager.instance.isAlert = false;
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
