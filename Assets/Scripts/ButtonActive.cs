using UnityEngine;
using Photon.Pun;
// When a player collides with an object or gets close to it, Button is active
// portal: close
// clue: collide

public class ButtonActive : MonoBehaviour
{
    public string playerTag = "Player";  // Player tag
    public GameObject buttonObject;   // clueButton, codeButton
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // 초기에 Rigidbody2D를 Static으로 설정

        buttonObject.SetActive(false);
    }

    ///  clue
    private void OnCollisionStay2D(Collision2D collision)       // If local player's tag is Infect, buttonObject is Unactive
    {
        if (collision.collider.CompareTag("Infect") && collision.collider.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("Collider Infected");
            buttonObject.SetActive(false);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(playerTag) && collision.collider.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("clue collision : " + collision.collider.GetComponent<PhotonView>().Owner.NickName);
            buttonObject.SetActive(true);
            // rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        {
            buttonObject.SetActive(false);
            // rb.bodyType = RigidbodyType2D.Static;
        }
    }

    /// portal

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && other.GetComponent<PhotonView>().IsMine)
        {
            buttonObject.SetActive(true);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Infect") && other.GetComponent<PhotonView>().IsMine)
        {
            buttonObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        {
            buttonObject.SetActive(false);
        }
    }

}

