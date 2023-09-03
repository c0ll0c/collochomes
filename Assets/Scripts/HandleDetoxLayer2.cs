using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandleDetoxLayer2 : MonoBehaviour
{
    public AudioSource HealSound;

    private string playerTag = "Infect"; // Player 태그
    public GameObject targetObject;     // 위치를 변경할 대상 오브젝트
    private Renderer detoxRenderer; // 해당 오브젝트의 렌더러

    public float delayTime = 5.0f;
    private Vector3[] randomPositions = new Vector3[]
    {
    new Vector3(1.9f, -8.77f, 0f),
    new Vector3(-0.36f, 9.03f, 0f),
    new Vector3(5.27f, 7.59f, 0f),
    new Vector3(13.42f, 4.38f, 0f),
    new Vector3(10.77f, -0.96f, 0f),
    new Vector3(-7.5f, 10.66f, 0f),
    new Vector3(-0.81f, -3.86f, 0f),
    new Vector3(-3.95f, -4.59f, 0f),
    new Vector3(4.57f, -3.45f, 0f),
    new Vector3(2.52f, 19.19f, 0f),
    new Vector3(5.49f, 20.41f, 0f),
    new Vector3(9.37f, 8f, 0f),
    new Vector3(-3.33f, 6.2f, 0f),
    };
    private PhotonView photonView;              // 다른 플레이어한테도 동기화

    private void Start()
    {
        detoxRenderer = GetComponent<Renderer>();

        Vector3 randomPosition = randomPositions[Random.Range(0, randomPositions.Length)];
        targetObject.transform.position = randomPosition;
        photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(playerTag))
        {
            HealSound.Play();

            photonView.TransferOwnership(collision.collider.GetComponent<PhotonView>().Owner);
            Debug.Log("해독!");

            // 랜덤 위치 후보 중 하나 선택
            Vector3 randomPosition = randomPositions[Random.Range(0, randomPositions.Length)];
            targetObject.transform.position = randomPosition;

            detoxRenderer.enabled = false; // 렌더러를 비활성화
            Debug.Log("해독제 비활성화");

            Invoke(nameof(ActivateDetox), delayTime); // delayTime 이후에 ActivateDetox 메서드 호출

        }
    }

    private void ActivateDetox()
    {
        detoxRenderer.enabled = true; // 렌더러를 활성화
        Debug.Log("해독제 활성화");
    }

}
