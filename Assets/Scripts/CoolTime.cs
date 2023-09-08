using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public Image image;
    public Button button;
    private float coolTime = 15.0f;              // 쿨타임 시간
    private bool isClicked = false;              // 공격 개시?
    float leftTime = 15.0f;                     // 남은 시간 (갱신)

    private void Start()
    {
        leftTime = 0;
        if (button)
            button.enabled = true;
        isClicked = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isClicked)                                      // 공격 시
            if (leftTime > 0)                               // 남은 시간이 있다면!!!
            {
                leftTime -= Time.deltaTime;         // 시간 줄이기 
                if (leftTime < 0)                           // 쿨타임 다 찼으면
                {
                    leftTime = 0;                           // 쿨타임 다 찼다고 표시
                    if (button)
                        button.enabled = true;
                    isClicked = true;                       //
                }

                float ratio = 1.0f - (leftTime / coolTime);
                if (image)
                    image.fillAmount = ratio;
            }
    }

    public void StartCoolTime()                         // 쿨타임 아직 안 찼으면 공격 무효
    {
        leftTime = coolTime;
        isClicked = true;
        if (button)
            button.enabled = false; // 버튼 기능을 해지함.
    }
}