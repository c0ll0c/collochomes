using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleDetoxLayer2 : MonoBehaviour
{
    public AudioSource HealSound;

    //public GameObject targetObject;     
    [SerializeField] Image image;
    [SerializeField] Image timer;

    //public float delayTime = 5.0f;
    private bool detoxActivated = true;
    private GameObject entry = null;
    private float leftTime = 3.0f;

    private Vector3[] randomPosLayer1 = new Vector3[]
    {
        new Vector3(10.0f, 14.0f, 0f),
        new Vector3(-3.0f, 17.0f, 0f),
        new Vector3(-0.2f, 17.5f, 0f),
        new Vector3(2.4f, 16.0f, 0f),
        new Vector3(-0.3f, 13.5f, 0f),
        new Vector3(4.0f, 5.0f, 0f),
        new Vector3(-1.4f, 5.0f, 0f),
        new Vector3(13.5f, 7.0f, 0f),
        new Vector3(-4.0f, 3.0f, 0f),
        new Vector3(-2.5f, 0.5f, 0f),
        new Vector3(-4.3f, -6.7f, 0f),
        new Vector3(4.0f, -9.3f, 0f),
        new Vector3 (11.8f, -6.8f, 0f),
        new Vector3(9.4f, -2.2f, 9f),
    };
    private Vector3[] randomPosLayer2 = new Vector3[]
    {
        new Vector3(-3.8f, 6.5f, 0f),
        new Vector3(-7f, 9.2f, 0f),
        new Vector3(-0.4f, 12.0f, 0f),
        new Vector3 (2.6f, 11.2f, 0f),
        new Vector3 (5.0f, 12.0f, 0f),
        new Vector3 (11.2f, 12.0f, 0f),
        new Vector3 (12.4f, 6.0f, 0f),
        new Vector3 (7.0f, 11.0f, 0f),
        new Vector3 (9.5f, 4.0f, 0f),
        new Vector3 (4.0f, -3.2f, 0f),
        new Vector3 (-7.2f, -3.0f, 0f),
        new Vector3 (-2.5f, -3.2f, 0f),
    };

    private PhotonView photonView;
    private RuntimeAnimatorController anim;

    private void Start()
    {
        timer.enabled = false;
        image.enabled = false;
        Vector3 randomPosition = randomPosLayer2[Random.Range(0, randomPosLayer2.Length)];
        transform.position = randomPosition;
        photonView = GetComponent<PhotonView>();
        anim = GetComponent<Animator>().runtimeAnimatorController;
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
                    gameObject.GetComponent<Animator>().runtimeAnimatorController = null;
                    StartCoroutine(ResetDetoxCoolDown());

                    if (entry.tag == "Infect" && entry.GetPhotonView().IsMine)
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Virus" || collision.gameObject.tag == "Infect") && detoxActivated)
        {
            entry = collision.gameObject;
            leftTime = 3.0f;
            timer.enabled = true;
            image.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        entry = null;
        timer.enabled = false;
        image.enabled = false;
    }

    private IEnumerator ResetDetoxCoolDown()   // 해독 비활성화
    {
        yield return new WaitForSeconds(15.0f);
        detoxActivated = true;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = anim;
    }
}

