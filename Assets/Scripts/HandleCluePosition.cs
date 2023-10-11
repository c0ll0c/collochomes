using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandleCluePosition : MonoBehaviour
{

    public GameObject[] UserClueObject; // 스폰할 오브젝트들의 배열
    public GameObject[] CodeClueObject; // 스폰할 오브젝트들의 배열
    public GameObject[] fakeObject; // 스폰할 단서 오브젝트 위치들의 배열

    public Vector3[] randomUserPositions = new Vector3[]
    {
    };
    public Vector3[] randomCodePositions = new Vector3[]
    {
    };

    public Vector3[] fakeRandomPositions = new Vector3[]    // 스폰할 속임수 오브젝트 위치들의 배열
    {

    };

    private PhotonView photonView;              // 다른 플레이어한테도 동기화

    void Start()
    {

        // randomPositions 배열을 랜덤으로 섞음
        ShuffleArray(randomUserPositions);
        ShuffleArray(randomCodePositions);
        ShuffleArray(fakeRandomPositions);

        // targetObject 배열의 각 오브젝트에 랜덤 위치 할당
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