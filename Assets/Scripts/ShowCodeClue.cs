using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ܼ� ���� ��ư�� ������ �� ��ȣ �ܼ��� ���̴� ��
// �ܼ� ȹ���� ����

public class ShowCodeClue : MonoBehaviour
{
    public AudioSource doneAudio;

    public GameObject CluePanelObject;                  // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;

    public Text Code;                         // ���� ���� �ִ� �ܼ�

    static public string code;

    public void ShowPanel()                                // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        if (!IsClicked)                                 // �̹� ȹ���� �ܼ��� �ƴϸ�
        {
            IsClicked = true;                          
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
        Debug.Log("�˶�" + GameManager.instance.isAlert);

        yield return new WaitForSeconds(1f); // 2�� ���

        GameManager.instance.isAlert = false;
        AlreadyPanelObject.SetActive(false);
        GameManager.instance.isAlert = false;
        Debug.Log("�˶�" + GameManager.instance.isAlert);
    }

    public void HidePanel()                                // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        doneAudio.Play();
        StartCoroutine(WaitForSoundToDone());

    }

    private IEnumerator WaitForSoundToDone()
    {
        yield return new WaitForSeconds(doneAudio.clip.length);

        GameManager.instance.isAlert = false;
        CluePanelObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
