using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 현재 충돌해 있는 

public class UserClue : MonoBehaviour
{
    // Start is called before the first frame update

    public Text usernameText_1;  // UI Text 요소를 참조하기 위한 변수
    public Text usernameText_2;  // UI Text 요소를 참조하기 위한 변수
    public Text usernameText_3;  // UI Text 요소를 참조하기 위한 변수
    public Text usernameText_4;  // UI Text 요소를 참조하기 위한 변수
    public Text usernameText_5;  // UI Text 요소를 참조하기 위한 변수
    public Text usernameText_6;  // UI Text 요소를 참조하기 위한 변수
    
    public Text usercodeText_1;  // UI Text 요소를 참조하기 위한 변수
    public Text usercodeText_2;  // UI Text 요소를 참조하기 위한 변수
    public Text usercodeText_3;  // UI Text 요소를 참조하기 위한 변수
    public Text usercodeText_4;  // UI Text 요소를 참조하기 위한 변수
    public Text usercodeText_5;  // UI Text 요소를 참조하기 위한 변수
    public Text usercodeText_6;  // UI Text 요소를 참조하기 위한 변수

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
            if (collision.gameObject.CompareTag("UserClue"))                    // 유저들의 코드를 갖고 있는 단서와 부딪혔을 때
            {
                Debug.Log("유저들의 코드 획득");
                // 단서 모음집의 이름 배열을 순회해서, 단서 판넬의 string과 일치하는 인덱스가 나오면, string을 코드 배열의 해당 인덱스에 저장
            }
        }*/

    private void Update()               
    {
        //Debug.Log(UserName[0]);

        if (ShowClue.username != null)         // a 단서가 이미 획득이 되었다면, UserName_a가 몇 번째 인덱스에 존재하는지 찾아야 함
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
