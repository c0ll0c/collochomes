using UnityEngine;
using Photon.Pun;
// When a player collides with an object or gets close to it, Button is active
// portal: close
// clue: collide

public class ButtonActive : MonoBehaviour
{
    public string playerTag = "Player";  // Player tag
    public GameObject buttonObject;   // clueButton, codeButton

    private void Start()
    {
        buttonObject.SetActive(false);
    }

    ///  clue

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collision.collider.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("clue collision : " + collision.collider.GetComponent<PhotonView>().Owner.NickName);
            buttonObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        {
            buttonObject.SetActive(false);
        }
    }

    /// portal

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PhotonView>().IsMine)
        {
            buttonObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        {
            buttonObject.SetActive(false);
        }
    }

}

