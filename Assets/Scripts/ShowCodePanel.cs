using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCodePanel : MonoBehaviour
{
    public GameObject CodePanelObject;       // ���̰� �� �ܼ� �ǳ��� �Ҵ��� ��
    public string correctCode = "hi";        // ���� ��ȣ ����

    public InputField inputField;            // InputField ����

    public void ShowPanel()                  // �ܼ� ���� ��ư�� ������ �ǳ��� ����
    {
        GameManager.instance.isAlert = true;
        CodePanelObject.SetActive(true);
    }

    public void CancelButton()               // �ݱ� ��ư�� ������ �ǳ��� �� ����
    {
        GameManager.instance.isAlert = false;
        CodePanelObject.SetActive(false);
        
    }

    public void DoneButton()
    {
        string enteredCode = inputField.text;   // InputField�� �ؽ�Ʈ �� ��������

        if (enteredCode == correctCode)         // �Է��� �ڵ尡 ���� �ڵ�� ��ġ�ϴ��� Ȯ��
        {
            Debug.Log("�����Դϴ�! �� ��ȯ �Ǵ� �ٸ� ���� ����");
        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�. �ٽ� �õ��ϼ���.");
        }
    }
}
