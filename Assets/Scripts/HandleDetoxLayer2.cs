using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleDetoxLayer2 : MonoBehaviour
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
