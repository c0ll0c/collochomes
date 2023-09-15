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

    private void Start()
    {
        leftTime = 0;
        if (button)
            button.enabled = true;
        isClicked = false;
    }
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
                    isClicked = false;                       //
                }

                float ratio = 1.0f - (leftTime / coolTime);
                if (image)
                    image.fillAmount = ratio;
            }
    }

    private void OnEnable()
    {
        leftTime = 0;
        if (button)
            button.enabled = true;
        isClicked = false;
        if (image)
            image.fillAmount = 1.0f;

        if (GameManager.instance == null || GameManager.instance.gamePlayer == null) return;
        GameManager.instance.gamePlayer.transform.Find("player trigger").GetComponent<AttackController>().attackActivated = false;
        Debug.Log(GameManager.instance.gamePlayer.transform.Find("player trigger").GetComponent<AttackController>().attackActivated);
    }

    public void StartCoolTime()                         // ��Ÿ�� ���� �� á���� ���� ��ȿ
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false; // ��ư ����� ������.
    }
}