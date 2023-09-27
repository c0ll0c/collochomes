using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

// If collision player infected, the panel is unactive
// 홈즈가 단서랑 부딪히면 단서 보기 버튼이 뜨고, 그 버튼을 누르면 단서 판넬이 뜸

public class CluePanelActive : MonoBehaviour
{
    public AudioSource doneAudio;

    public GameObject CluePanelObject;               
    public GameObject AlreadyPanelObject;
    public GameObject HiddenCluePanel;

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
        if(HiddneClue.IsHidden)         // 숨겨진 단서면 숨겨진 단서라는 판넬이 뜸
        {
            GameManager.instance.isAlert = true;
            HiddenCluePanel.SetActive(true);
            StartCoroutine(DeactivateHiddenCluePanel());
            return;
        }

        // 숨겨진 단서가 아닐 때!

        else if (!IsClicked)                       
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
        if (HiddneClue.IsHidden)         // 숨겨진 단서면 숨겨진 단서라는 판넬이 뜸
        {
            GameManager.instance.isAlert = true;
            HiddenCluePanel.SetActive(true);
            StartCoroutine(DeactivateHiddenCluePanel());
            return;
        }
        
        // 숨겨진 단서가 아닐 때!

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
    IEnumerator DeactivateHiddenCluePanel()
    {
        yield return new WaitForSeconds(1f);
        HiddenCluePanel.SetActive(false);
        GameManager.instance.isAlert = false;
    }
}
