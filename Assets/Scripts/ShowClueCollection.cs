using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����ó�� ���� ��ư ������ �ܼ� ������ â�� �߰� �ϴ� ��ũ��Ʈ
// ���� ��ư�� �Ҵ��� �ֱ�

public class ShowClueCollection : MonoBehaviour
{
    public GameObject button;       // ����ó�� ���� ��ư
    public GameObject CollectionPanel;        // �ܼ� ������ �ǳ�

    // Start is called before the first frame update
    void Start()
    {
        CollectionPanel.SetActive(false);
    }

    public void ShowCollection ()
    {
        CollectionPanel.SetActive(true);            // ��ư ������ �ܼ� ������ �ǳ� ���ü��� ���̰� �ϱ�
        // isAlert = true�� �ؼ� ĳ���� �� �����̰� �� ��
    }

    public void CloseCollection() {
        CollectionPanel.SetActive(false);
        // isAlert = false�� �ؼ� ĳ���� ������ �� �ְ�
    }

}
