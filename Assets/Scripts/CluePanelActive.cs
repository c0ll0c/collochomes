using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// If collision player infected, the panel is unactive

public class CluePanelActive : MonoBehaviour
{
    public AudioSource doneAudio;

    public GameObject CluePanelObject;               
    public GameObject AlreadyPanelObject;

    public bool IsClicked = false;

    /*public Text Code;
    public Text UserName;
    public Text UserCode;*/
    public Text[] Text;

    static public string code;
    static public string username;
    static public string usercode;
    private void OnCollisionStay2D(Collision2D collision)       // If local player's tag is Infect, buttonObject is Unactive
    {
        if (collision.collider.CompareTag("Infect") && collision.collider.GetComponent<PhotonView>().IsMine)
        {
            // Debug.Log("Collider Infected");
            CluePanelObject.SetActive(false);
            GameManager.instance.isAlert = false;
        }
    }
    void Update()
    {
    }

    public void ShowCodeCluePanel()                          
    {
        if (!IsClicked)                       
        {
            IsClicked = true;                          
            GameManager.instance.isAlert = true;
            CluePanelObject.SetActive(true);

            GotClue.count++;                 
            // code = Code.text;
            code = Text[0].text;
        }

        else                       
        {
            if (!GameManager.instance.isAlert)                  
                StartCoroutine(DeactivateAlreadyPanel());
        }
    }

    public void ShowUserPanel()
    {
        if (!IsClicked)
        {
            IsClicked = true;
            GameManager.instance.isAlert = true;
            CluePanelObject.SetActive(true);
        }

        else
        {
            if (!GameManager.instance.isAlert)
                StartCoroutine(DeactivateAlreadyPanel());
        }
        // username = UserName.text;
        username = Text[0].text;
        // usercode = UserCode.text;
        usercode = Text[1].text;
    }

    IEnumerator DeactivateAlreadyPanel()
    {
        AlreadyPanelObject.SetActive(true);
        GameManager.instance.isAlert = true;
        Debug.Log("�˶�" + GameManager.instance.isAlert);

        yield return new WaitForSeconds(1f); 

        GameManager.instance.isAlert = false;
        AlreadyPanelObject.SetActive(false);
        Debug.Log("�˶�" + GameManager.instance.isAlert);
    }

    public void HidePanel()                       
    {
        doneAudio.Play();
        StartCoroutine(WaitForSoundToDone());

    }

    private IEnumerator WaitForSoundToDone()
    {
        yield return new WaitForSeconds(doneAudio.clip.length);

        GameManager.instance.isAlert = false;
        CluePanelObject.SetActive(false);
    }
}
