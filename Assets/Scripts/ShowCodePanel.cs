using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public GameObject GameOverObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��

    public string correctCode;        // ���� ��ȣ ����
    public InputField inputField;            // InputField ����

    private void Start()
    {
        GameOverObject = GameObject.Find("Canvas").gameObject;
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
            Debug.Log("�����Դϴ�! �� ��ȯ �Ǵ� �ٸ� ���� ����");

            GameOverObject.SetActive(true);
            // ������ ��� GameOverScene���� �� ��ȯ
            //SceneManager.LoadScene("GameOverScene"); // "GameOverScene"�� ������ ����ϴ� ���� �̸����� �����ؾ� �մϴ�.
        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�. �ٽ� �õ��ϼ���.");
        }
    }
}
