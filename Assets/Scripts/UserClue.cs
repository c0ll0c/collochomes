using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ���� �浹�� �ִ� 

public class UserClue : MonoBehaviour
{
    // Start is called before the first frame update

    public Text usernameText_1;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usernameText_2;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usernameText_3;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usernameText_4;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usernameText_5;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usernameText_6;  // UI Text ��Ҹ� �����ϱ� ���� ����
    
    public Text usercodeText_1;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usercodeText_2;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usercodeText_3;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usercodeText_4;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usercodeText_5;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text usercodeText_6;  // UI Text ��Ҹ� �����ϱ� ���� ����

    public string[] UserName = new string[6];
    public string[] UserCode = new string[6];

    int index;

    void Start()
    {
        UserName[0] = usernameText_1.text;
        UserName[1] = usernameText_2.text;
        UserName[2] = usernameText_3.text;
        UserName[3] = usernameText_4.text;
        UserName[4] = usernameText_5.text;
        UserName[5] = usernameText_6.text;

        UserCode[0] = usercodeText_1.text;
        UserCode[1] = usercodeText_2.text;
        UserCode[2] = usercodeText_3.text;
        UserCode[3] = usercodeText_4.text;
        UserCode[4] = usercodeText_5.text;
        UserCode[5] = usercodeText_6.text;

/*        Debug.Log(UserName[0] + UserName[1] + UserName[2] + UserName[3] + UserName[4] + UserName[5]);
        Debug.Log(UserCode[0] + UserCode[1] + UserCode[2] + UserCode[3] + UserCode[4] + UserCode[5]);*/
    }
    /*
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("UserClue"))                    // �������� �ڵ带 ���� �ִ� �ܼ��� �ε����� ��
            {
                Debug.Log("�������� �ڵ� ȹ��");
                // �ܼ� �������� �̸� �迭�� ��ȸ�ؼ�, �ܼ� �ǳ��� string�� ��ġ�ϴ� �ε����� ������, string�� �ڵ� �迭�� �ش� �ε����� ����
            }
        }*/

    private void Update()               
    {
        //Debug.Log(UserName[0]);

        if (ShowClue.username != null)         // a �ܼ��� �̹� ȹ���� �Ǿ��ٸ�, UserName_a�� �� ��° �ε����� �����ϴ��� ã�ƾ� ��
        {
            for(int i = 0; i < 6; i++)
            {
                if (UserName[i] == ShowClue.username)
                {
                    index = i;
                }
            }
            if (index == 0)
                usercodeText_1.text = ShowClue.usercode;
            if (index == 1)
                usercodeText_2.text = ShowClue.usercode;
            if (index == 2)
               usercodeText_3.text = ShowClue.usercode;
            if (index == 3)
                usercodeText_4.text = ShowClue.usercode;
            if (index == 4)
                usercodeText_5.text = ShowClue.usercode;
            if (index == 5)
                usercodeText_6.text = ShowClue.usercode;
        }
    }
}
