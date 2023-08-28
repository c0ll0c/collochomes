using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoolTime : MonoBehaviour
{
    public Image image;
    public Button button;
    public float coolTime = 10.0f;              // 쿨타임 시간
    public bool isClicked = false;              // 공격 개시?
    float leftTime = 10.0f;                     // 남은 시간 (갱신)
    float speed = 5.0f;                         // 이건 뭐지...? 쿨타임 속도...? 그게 왜 필요하노

    // Update is called once per frame
    void Update()
    {
        if (isClicked)                                      // 공격 시
            if (leftTime > 0)                               // 남은 시간이 있다면!!!
            {
                leftTime -= Time.deltaTime * speed;         // 시간 줄이기 
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