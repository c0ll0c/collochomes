using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player -> Infect, The panel is inactive
// �ܼ� ������ �ǳ��� ���� ���·� ���������� �� ��� 

public class CollectionPanelInavtive : MonoBehaviour
{
    static public bool IsInfected = false;
    public GameObject CollectionPanel;        // �ܼ� ������ �ǳ�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInfected)
        {
            CollectionPanel.SetActive(false);
            GameManager.instance.isAlert = false;
        }
    }
}
