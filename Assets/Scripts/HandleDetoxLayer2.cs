using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandleDetoxLayer2 : MonoBehaviour
{
    public AudioSource HealSound;

    private string playerTag = "Infect"; // Player �±�
    public GameObject targetObject;     // ��ġ�� ������ ��� ������Ʈ
    private Renderer detoxRenderer; // �ش� ������Ʈ�� ������

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
    private PhotonView photonView;              // �ٸ� �÷��̾����׵� ����ȭ

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

            Debug.Log("Detox");
            StartCoroutine(detoxStatus());

            // ���� ��ġ �ĺ� �� �ϳ� ����
            Vector3 randomPosition = randomPositions[Random.Range(0, randomPositions.Length)];
            targetObject.transform.position = randomPosition;

            detoxRenderer.enabled = false; // �������� ��Ȱ��ȭ
            Debug.Log("�ص��� ��Ȱ��ȭ");

            Invoke(nameof(ActivateDetox), delayTime); // delayTime ���Ŀ� ActivateDetox �޼��� ȣ��

        }
    }

    private void ActivateDetox()
    {
        detoxRenderer.enabled = true; // �������� Ȱ��ȭ
        Debug.Log("�ص��� Ȱ��ȭ");
    }

    private IEnumerator detoxStatus()   // 해독 지연
    {
        yield return new WaitForSeconds(1.0f);
        NetworkManager.instance.SetPlayerStatus("Player");
    }

}
