using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// X�� �������� ���ư���, ���̱����ض� �浹 �� ���̱����� Ȱ��ȭ

public class Plane : MonoBehaviour
{
    public GameObject plane;
    public AudioSource PlaneAudio;
    public GameObject[] vaccine;
    public Vector2[] planePosition = new Vector2[]{
    };
    public float RandomTime;
    private PhotonView photonView;

    void Start()
    {
        RandomTime = Random.Range(10, 250);
        Debug.Log(RandomTime);

        photonView = GetComponent<PhotonView>();

        ShuffleArray(planePosition);// ����� ���ư��� �����ϴ� ��ġ shuffle �ؼ� ���߿� �ϳ� currentPosition���� ������ �θ� �� ��
        plane.transform.position = planePosition[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (CountDown.IsTimerRunning)
        {
            Invoke("MovePlaneLeft", RandomTime);

            for (int i = 0; i < vaccine.Length; i++)
            {
                if (Vector2.Distance(plane.transform.position, vaccine[i].transform.position) <= 0.5f)
                {
                    Debug.Log("��� ��ô! �Ÿ�: " + Vector2.Distance(plane.transform.position, vaccine[i].transform.position));
                    vaccine[i].SetActive(true);
                }
            }

            Invoke("InactivePlane", RandomTime + 20.0f);
        }
    }

    void MovePlaneLeft() {
        PlaneAudio.Play();

        Vector2 currentPosition = plane.transform.position;
        currentPosition.x -= 2.0f * Time.deltaTime;

        plane.transform.position = currentPosition;
    }

    void ShuffleArray(Vector2[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Vector2 temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void InactivePlane()
    {
        plane.SetActive(false);
    }

}
