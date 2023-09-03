using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

// Ż�� -> ���� �÷��̾�: �� / ���� �÷��̾� : ��
// ���� �÷��̾��� IsMineEscape = true, ���� �÷��̾��� IsOtherEscape = true�� �ؼ�... IsMineEscape�� true�̸� Win, IsOtherEscape�� true�̸� Lose
// ������ Ż���ߴ°� / Ż���� �ߴ°�

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��

    public string correctCode;        // ���� ��ȣ ����
    public InputField inputField;            // InputField ����

    static public bool IsMineEscape = false;
    static public bool IsEscape = false;

    private void Start()
    {
    }

    private void Update()
    {
        correctCode = CodeCluePanel.virusCode;
    }

    public void ShowPanel()                  // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()               // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        CodePanelObject.SetActive(false);
    }

    public void DoneButton()
    {
        string enteredCode = inputField.text;   // InputField�� �ؽ�Ʈ �� ��������

        Debug.Log("�Էµ� ��ȣ" + enteredCode);
        Debug.Log("����" + correctCode);

        if (enteredCode == correctCode)         // �Է��� �ڵ尡 ���� �ڵ�� ��ġ�ϴ��� Ȯ��
        {
            Debug.Log("����! WIN");
            IsMineEscape = true;
            IsEscape = true;                            // �긦 ����ȭ������� ��...
        }

        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�. �ٽ� �õ��ϼ���.");
        }

    }
}
