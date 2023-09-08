using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public Image image;
    public Button button;
    private float coolTime = 15.0f;              // ��Ÿ�� �ð�
    private bool isClicked = false;              // ���� ����?
    float leftTime = 15.0f;                     // ���� �ð� (����)

    // Update is called once per frame
    void Update()
    {
        if (isClicked)                                      // ���� ��
            if (leftTime > 0)                               // ���� �ð��� �ִٸ�!!!
            {
                leftTime -= Time.deltaTime;         // �ð� ���̱� 
                if (leftTime < 0)                           // ��Ÿ�� �� á����
                {
                    leftTime = 0;                           // ��Ÿ�� �� á�ٰ� ǥ��
                    if (button)
                        button.enabled = true;
                    isClicked = true;                       //
                }

                float ratio = 1.0f - (leftTime / coolTime);
                if (image)
                    image.fillAmount = ratio;
            }
    }

    public void StartCoolTime()                         // ��Ÿ�� ���� �� á���� ���� ��ȿ
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false; // ��ư ����� ������.
    }
}