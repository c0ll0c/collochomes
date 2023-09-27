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

    private PhotonView photonView;
    private RuntimeAnimatorController anim;

    private void Start()
    {
        timer.enabled = false;
        image.enabled = false;
        Vector3 randomPosition = randomPositions[Random.Range(0, randomPositions.Length)];
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

