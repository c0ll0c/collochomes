using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandleCluePositionLayer2 : MonoBehaviour
{

    public GameObject[] UserClueObjectLayer2; // 스폰할 오브젝트들의 배열
    public GameObject[] CodeClueObjectLayer2; // 스폰할 오브젝트들의 배열
    public GameObject[] fakeObjectLayer2; // 스폰할 단서 오브젝트 위치들의 배열

    public Vector3[] randomUserPositionsLayer2 = new Vector3[]
    {
    };
    public Vector3[] randomCodePositionsLayer2 = new Vector3[]
    {
    };

    public Vector3[] fakeRandomPositionsLayer2 = new Vector3[]    // 스폰할 속임수 오브젝트 위치들의 배열
    {

    };

    private PhotonView photonView;              // 다른 플레이어한테도 동기화

    void Start()
    {

        // randomPositions 배열을 랜덤으로 섞음
        ShuffleArray(randomUserPositionsLayer2);
        ShuffleArray(randomCodePositionsLayer2);
        ShuffleArray(fakeRandomPositionsLayer2);

        // targetObject 배열의 각 오브젝트에 랜덤 위치 할당
        for (int i = 0; i < UserClueObjectLayer2.Length; i++)
        {
            Debug.Log("단서 개수" + UserClueObjectLayer2.Length);
            UserClueObjectLayer2[i].transform.localPosition = randomUserPositionsLayer2[i];
        }
        
        for (int i = 0; i < CodeClueObjectLayer2.Length; i++)
        {
            Debug.Log("단서 개수" + CodeClueObjectLayer2.Length);
            CodeClueObjectLayer2[i].transform.localPosition = randomCodePositionsLayer2[i];
        }
        
        for (int i = 0; i < fakeObjectLayer2.Length; i++)
        {
            fakeObjectLayer2[i].transform.localPosition = fakeRandomPositionsLayer2[i];
        }
        
        photonView = GetComponent<PhotonView>();

    }

    // 배열을 무작위로 섞는 함수
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