using UnityEngine;
using Photon.Pun;
// 단서 보기 버튼이 뜨게 하는 스크립트

public class HandleClueButton : MonoBehaviour
{
    public string playerTag = "Player";  // Player 태그
    public GameObject clueObject;   // 단서 오브젝트
    public GameObject buttonObject;   // 단서 보기 버튼

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 충돌 시 버튼 보임

    private void Start()
    {
        buttonObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collision.collider.GetComponent<PhotonView>().IsMine) // 플레이어 태그인 오브젝트와 충돌 시
        {
            Debug.Log("clue collision : " + collision.collider.GetComponent<PhotonView>().Owner.NickName);
            buttonObject.SetActive(true); // 단서 보기 버튼을 보이도록 설정
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // 플레이어 태그인 오브젝트와의 충돌이 해제될 때
        {
            buttonObject.SetActive(false); // 단서 보기 버튼을 감추도록 설정
        }
    }

}

// 단서와 가까웠다가, 멀어졌다가, 다시 가까워졌을 때에 작동하지 않음
// 처음부터 단서와 멀었다가, 가까워졌을 때 작동하지 않음
// 왜 isVisible이 false가 되면 update 함수가 더 이상 작동하지 않는 걸까...?
