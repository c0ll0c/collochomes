using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandleCluePositionLayer2 : MonoBehaviour
{

    public GameObject[] UserClueObjectLayer2; // ������ ������Ʈ���� �迭
    public GameObject[] CodeClueObjectLayer2; // ������ ������Ʈ���� �迭
    public GameObject[] fakeObjectLayer2; // ������ �ܼ� ������Ʈ ��ġ���� �迭

    public Vector3[] randomUserPositionsLayer2 = new Vector3[]
    {
    };
    public Vector3[] randomCodePositionsLayer2 = new Vector3[]
    {
    };

    public Vector3[] fakeRandomPositionsLayer2 = new Vector3[]    // ������ ���Ӽ� ������Ʈ ��ġ���� �迭
    {

    };

    private PhotonView photonView;              // �ٸ� �÷��̾����׵� ����ȭ

    void Start()
    {

        // randomPositions �迭�� �������� ����
        ShuffleArray(randomUserPositionsLayer2);
        ShuffleArray(randomCodePositionsLayer2);
        ShuffleArray(fakeRandomPositionsLayer2);

        // targetObject �迭�� �� ������Ʈ�� ���� ��ġ �Ҵ�
        for (int i = 0; i < UserClueObjectLayer2.Length; i++)
        {
            Debug.Log("�ܼ� ����" + UserClueObjectLayer2.Length);
            UserClueObjectLayer2[i].transform.localPosition = randomUserPositionsLayer2[i];
        }
        
        for (int i = 0; i < CodeClueObjectLayer2.Length; i++)
        {
            Debug.Log("�ܼ� ����" + CodeClueObjectLayer2.Length);
            CodeClueObjectLayer2[i].transform.localPosition = randomCodePositionsLayer2[i];
        }
        
        for (int i = 0; i < fakeObjectLayer2.Length; i++)
        {
            fakeObjectLayer2[i].transform.localPosition = fakeRandomPositionsLayer2[i];
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