using UnityEngine;
using Photon.Pun;

// 암호 입력 버튼이 뜨게 하는 스크립트

public class HandleCodeButton : MonoBehaviour
{
    public string playerTag = "Player";  // Player 태그
    public GameObject portal;   // 포털 오브젝트
    public GameObject buttonObject;   // 암호 입력 버튼


    ////////////////////////////////////////////////////////////////////////////////////////////// 가까워지면 버튼 보임

    /*    public float maxDistanceToShow = 1.0f; // 일정 거리

        private bool isVisible = false; // 오브젝트의 가시성 상태

        private void Start()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

            if (playerObject != null)
            {
                float distance = Vector3.Distance(playerObject.transform.position, portal.transform.position);
                if (distance <= maxDistanceToShow)
                {
                    isVisible = true;
                    buttonObject.SetActive(true); // 오브젝트를 보이도록 설정
                }
                else
                {
                    isVisible = false;
                    buttonObject.SetActive(false); // 오브젝트를 감추도록 설정
                }
            }
        }

        private void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

            // 플레이어와 오브젝트 사이의 거리 계산
            float distance = Vector3.Distance(playerObject.transform.position, portal.transform.position);

    *//*        Debug.Log(distance);
            Debug.Log(isVisible);*//*

            // 일정 거리 내에 플레이어가 있다면 가시성을 활성화
            if (distance <= maxDistanceToShow)
            {
               // Debug.Log("단서 발견");

               if (!isVisible)             // 안 보이고 있었다면
                {
                    isVisible = true;
                    buttonObject.SetActive(true); // 오브젝트를 보이도록 설정
                }
            }


            // 일정 거리 내에 플레이어가 있지 않다면 가시성을 비활성화
            else
            {
               // Debug.Log("단서로부터 멀어짐");

                if (isVisible)              // 보이고 있었다면
                {
                    isVisible = false;
                    buttonObject.SetActive(false); // 오브젝트를 감추도록 설정
                }

            }

        }
    */

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 충돌 시 버튼 보임

    private void Start()
    {
        buttonObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine) // 플레이어 태그인 오브젝트와 충돌 시
        {
            buttonObject.SetActive(true); // 암호 입력 버튼을 보이도록 설정
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어 태그인 오브젝트와의 충돌이 해제될 때
        {
            buttonObject.SetActive(false); // 암호 입력 버튼을 감추도록 설정
        }
    }

}
