using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �ܼ� ���� ��ư�� ������ CluePanel�� ���ü��� Ȱ��ȭ�� ��
// �ѹ� �ܼ� ���� ��ư�� ��������, "�̹� ȹ���� �ܼ�"��� �˸��� ���� ��

public class ShowClue : MonoBehaviour
{
    public AudioSource doneAudio;

    public GameObject CluePanelObject;                  // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;
    static public bool IsAlert = false;           // �������� �Ҵ�

    public Text UserName;                         // ���� ���� �ִ� �ܼ��� ���� �̸�
    public Text UserCode;                         // ���� ���� �ִ� �ܼ��� ���� �ڵ�

    static public string username;
    static public string usercode;

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
        username = UserName.text;                       // Text ������Ʈ�� �ִ� �ؽ�Ʈ�� ��ũ��Ʈ���� ����� �� �ֵ��� ������ �Ҵ�, �ܼ� ������ ��ũ��Ʈ���� ���, 
        usercode = UserCode.text;
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
        doneAudio.Play();
        StartCoroutine(WaitForSoundToDone());

    }

    private IEnumerator WaitForSoundToDone()
    {
        yield return new WaitForSeconds(doneAudio.clip.length);

        CluePanelObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
