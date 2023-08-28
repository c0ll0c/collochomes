using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class MoveObjectOnPlayerApproach : MonoBehaviour
{
    public string playerTag = "Virus"; // Player �±�
    public GameObject targetObject;     // ��ġ�� ������ ��� ������Ʈ
    public TilemapCollider2D tilemapCollider; // Ÿ�ϸ��� TilemapCollider2D
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
            Debug.Log("�ص�!");
            Photon.Realtime.Player targetPlayer = collision.collider.GetComponent<PhotonView>().Owner;
            //photonView.RPC("DetoxRPC", targetPlayer);
            //photonView.RPC("UpdateInfo", RpcTarget.All);

            // ���ѵ� ���� ������ ���� ��ġ ����
            //Vector3 randomPosition = GetRandomPositionInTilemapBounds();
            //targetObject.transform.position = randomPosition;
        }
    }

    private Vector3 GetRandomPositionInTilemapBounds()
    {
        Bounds bounds = tilemapCollider.bounds;
        Vector2 tilemapMin = bounds.min;
        Vector2 tilemapMax = bounds.max;

        // Ÿ�ϸ� ���� ������ ���� ��ġ ����
        float randomX = Random.Range(tilemapMin.x, tilemapMax.x);
        float randomY = Random.Range(tilemapMin.y, tilemapMax.y);
        Vector3 randomPosition = new Vector3(randomX, randomY, 0f);

        return randomPosition;
    }
}
