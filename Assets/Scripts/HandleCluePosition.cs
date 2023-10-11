using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandleCluePosition : MonoBehaviour
{

    public GameObject[] UserClueObject; // ������ ������Ʈ���� �迭
    public GameObject[] CodeClueObject; // ������ ������Ʈ���� �迭
    public GameObject[] fakeObject; // ������ �ܼ� ������Ʈ ��ġ���� �迭

    public Vector3[] randomUserPositions = new Vector3[]
    {
    };
    public Vector3[] randomCodePositions = new Vector3[]
    {
    };

    public Vector3[] fakeRandomPositions = new Vector3[]    // ������ ���Ӽ� ������Ʈ ��ġ���� �迭
    {

    };

    private PhotonView photonView;              // �ٸ� �÷��̾����׵� ����ȭ

    void Start()
    {

        // randomPositions �迭�� �������� ����
        ShuffleArray(randomUserPositions);
        ShuffleArray(randomCodePositions);
        ShuffleArray(fakeRandomPositions);

        // targetObject �迭�� �� ������Ʈ�� ���� ��ġ �Ҵ�
        for (int i = 0; i < UserClueObject.Length; i++)
        {
            UserClueObject[i].transform.localPosition = randomUserPositions[i];
        }
        
        for (int i = 0; i < CodeClueObject.Length; i++)
        {
            CodeClueObject[i].transform.localPosition = randomCodePositions[i];
        }
        
        for (int i = 0; i < fakeObject.Length; i++)
        {
            fakeObject[i].transform.localPosition = fakeRandomPositions[i];
        }
        
        photonView = GetComponent<PhotonView>();

    }

    // �迭�� �������� ���� �Լ�
    void ShuffleArray(Vector3[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Vector3 temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}