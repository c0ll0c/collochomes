using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class MoveObjectOnPlayerApproach : MonoBehaviour
{
    public string playerTag = "Virus"; // Player 태그
    public GameObject targetObject;     // 위치를 변경할 대상 오브젝트
    public TilemapCollider2D tilemapCollider; // 타일맵의 TilemapCollider2D
    private PhotonView photonView;

    private void Start()
    {
        //photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.CompareTag(playerTag))
        {
            Debug.Log("해독!");
            Photon.Realtime.Player targetPlayer = collision.collider.GetComponent<PhotonView>().Owner;
            //photonView.RPC("DetoxRPC", targetPlayer);
            //photonView.RPC("UpdateInfo", RpcTarget.All);

            // 제한된 영역 내에서 랜덤 위치 생성
            //Vector3 randomPosition = GetRandomPositionInTilemapBounds();
            //targetObject.transform.position = randomPosition;
        }
    }

    private Vector3 GetRandomPositionInTilemapBounds()
    {
        Bounds bounds = tilemapCollider.bounds;
        Vector2 tilemapMin = bounds.min;
        Vector2 tilemapMax = bounds.max;

        // 타일맵 영역 내에서 랜덤 위치 생성
        float randomX = Random.Range(tilemapMin.x, tilemapMax.x);
        float randomY = Random.Range(tilemapMin.y, tilemapMax.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        return randomPosition;
    }
}
