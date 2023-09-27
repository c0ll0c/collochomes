using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleDetox : MonoBehaviour
{
    public AudioSource HealSound;

    public GameObject targetObject;     // ��ġ�� ������ ��� ������Ʈ
    private Renderer detoxRenderer; // �ش� ������Ʈ�� ������
    [SerializeField] Image image;
    [SerializeField] Image timer;

    //public float delayTime = 5.0f;
    private bool detoxActivated = true;
    private string entry = null;
    private float leftTime = 3.0f;

    private Vector3[] randomPositions = new Vector3[]
    {
    new Vector3(6.94f, -3.59f, 0f),
    new Vector3(2.86f, 1.09f, 0f),
    new Vector3(2.46f, 0.64f, 0f),
    new Vector3(-0.372f, 3.858f, 0f),
    new Vector3(8.21f, -1.7f, 0f),
    new Vector3(-2.39f, -8.01f, 0f),
    new Vector3(14.73f, 2.04f, 0f),
    new Vector3(13.27f, 6.53f, 0f),
    new Vector3(12.46f, -7.41f, 0f),
    new Vector3(7.51f, -9.68f, 0f),
    new Vector3(-9f, -6.15f, 0f),
    new Vector3(-5.36f, 1.93f, 0f),
    new Vector3(-4.96f, 1.78f, 0f),
    new Vector3(-2.23f, -2.46f, 0f),
    new Vector3(4.13f, 5.01f, 0f),
    new Vector3(6.96f, 8.6f, 0f),
    new Vector3(14.803f, 6.259f, 0f),
    new Vector3(2.46f, 0.64f, 0f),
    new Vector3(11.84f, -7.95f, 0f),
    new Vector3(9.52f, -0.9f, 0f),
    new Vector3(6.93f, 8.53f, 0f),
    new Vector3(-4.86f, 2.16f, 0f),
    new Vector3(-0.68f, 3.34f, 0f),
    new Vector3(-7.95f, 0.7f, 0f),
    new Vector3(-9.8f, -6.49f, 0f),
    new Vector3(-4.87f, -9.18f, 0f),
    new Vector3(-3.12f, -9.87f, 0f),
    new Vector3(-9.26f, -5.37f, 0f),
    new Vector3(-8.5f, -7.16f, 0f),
    new Vector3(0.35f, 13.89f, 0f),
    new Vector3(7.13f, 15.42f, 0f),
    new Vector3(6.31f, 11.43f, 0f),
    new Vector3(-0.23f, 12.53f, 0f),
    new Vector3(-3.17f, 14.14f, 0f),
    new Vector3(10.23f, 13.9f, 0f),
    };
    private PhotonView photonView;              // �ٸ� �÷��̾����׵� ����ȭ
    private void Start()
    {
        detoxRenderer = GetComponent<Renderer>();
        timer.enabled = false;
        image.enabled = false;
        Vector3 randomPosition = randomPositions[Random.Range(0, randomPositions.Length)];
        targetObject.transform.position = randomPosition;
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (entry != null)
        {
            if (leftTime > 0)                               // 남은 시간이 있다면!!!
            {
                leftTime -= Time.deltaTime;         // 시간 줄이기 
                if (leftTime < 0)                           // 쿨타임 다 찼으면
                {
                    leftTime = 0;
                    image.enabled = false;
                    timer.enabled = false;
                    detoxActivated = false;
                    StartCoroutine(ResetDetoxCoolDown());

                    if (entry == "Infect")
                    {
                        HealSound.Play();
                        NetworkManager.instance.SetPlayerStatus("Player");
                    }
                }

                float ratio = 1.0f - (leftTime / 3.0f);
                if (image)
                    image.fillAmount = ratio;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Virus" && detoxActivated)
        {
            entry = "Virus";
            leftTime = 3.0f;
            timer.enabled = true;
            image.enabled = true;
        }
        if (collision.collider.tag == "Infect" && detoxActivated)
        {
            entry = "Infect";
            leftTime = 3.0f;
            timer.enabled = true;
            image.enabled = true;


            /*
            HealSound.Play();
            //photonView.TransferOwnership(collision.collider.GetComponent<PhotonView>().Owner);
            Debug.Log("Detox");
            StartCoroutine(detoxStatus());
            
            Vector3 randomPosition = randomPositions[Random.Range(0, randomPositions.Length)];
            targetObject.transform.position = randomPosition;
            detoxRenderer.enabled = false; // �������� ��Ȱ��ȭ
            Debug.Log("�ص��� ��Ȱ��ȭ");
            Invoke(nameof(ActivateDetox), delayTime); // delayTime ���Ŀ� ActivateDetox �޼��� ȣ��
            */
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        entry = null;
        timer.enabled = false;
        image.enabled = false;
    }
    /*
    private void ActivateDetox()
    {
        detoxRenderer.enabled = true; 
    }
    */

    private IEnumerator ResetDetoxCoolDown()   // 해독 지연
    {
        yield return new WaitForSeconds(15.0f);
        detoxActivated = true;
    }
}