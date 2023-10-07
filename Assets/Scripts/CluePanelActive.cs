using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

// If collision player infected, the panel is unactive
// 홈즈가 단서랑 부딪히면 단서 보기 버튼이 뜨고, 그 버튼을 누르면 단서 판넬이 뜸
// 모든 단서에 부착되어 있는 거... 음...

public class CluePanelActive : MonoBehaviour
{
    public AudioSource doneAudio;

    public GameObject CluePanelObject;
    public GameObject AlreadyPanelObject;
    public GameObject HiddenCluePanel;

    public bool IsClicked = false;
    public bool IsHidden;

    public Text[] Text;

    static public string code;
    static public string username;
    static public string usercode;
    float hiddenTime = 15.0f;
    private PhotonView photonView;
    int virusPlayerActorNumber = -1; // 초기값으로 -1을 설정하여 오류를 확인하기 쉽게 합니다.
    int i;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        StartCoroutine(ownershipRequest());
    }

    IEnumerator ownershipRequest()          // 게임 시작 5초 후, 바이러스 유저에게 소유권 양도
    {
        yield return new WaitForSeconds(5.0f);

        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        for (i = 0; i < currentPlayersStatus.Count; i++)  
        {
            if (currentPlayersStatus[i].PlayerStatus == "Virus")     
            {
                for (int j = 0; i < currentPlayersStatus.Count; j++)
                {
                    if (currentPlayersStatus[i].Nickname == PhotonNetwork.PlayerList[j].NickName)       
                    {
                        virusPlayerActorNumber = PhotonNetwork.PlayerList[j].ActorNumber;    
                        break;
                    }
                }
                break;
            }
        }

        Debug.Log(virusPlayerActorNumber);

        Player targetPlayer = PhotonNetwork.CurrentRoom.GetPlayer(virusPlayerActorNumber);

        photonView.TransferOwnership(targetPlayer);
        Debug.Log("이 사람이 소유권을 받았을 것으로 예상" + targetPlayer.ActorNumber);
        
        StartCoroutine(debug());
    }

    IEnumerator debug()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("현재 소유권을 가지고 있는 플레이어의 ID: " + photonView.Owner.ActorNumber);
    }

    [PunRPC]
    public void SyncHiddenVariable(bool ishidden)
    {
        Debug.Log("RPC 호출");
        // 변수 값을 동기화
        IsHidden = ishidden;
    }

    public void hiddenButtonClick()
    {
        Debug.Log("포톤 디버그 " + photonView.IsMine);

        if (photonView.IsMine)
        {
            //IsHidden = true;
            photonView.RPC("SyncHiddenVariable", RpcTarget.All, true);
            Debug.Log("단서 숨겨 버리기" + IsHidden);
            HiddenCluePanel.SetActive(true);                // 숨겨진 단서! 켜지기
            GameManager.instance.isAlert = true;
            StartCoroutine(DeactivateHiddenCluePanel());    // 숨겨진 단서! 꺼지기
            StartCoroutine(UnHiddenClue());
        }
    }

    IEnumerator UnHiddenClue()              // 15초 뒤에 단서 숨김 해제
    {
        yield return new WaitForSeconds(hiddenTime);
        IsHidden = false;
        Debug.Log("단서 숨김 해제" + IsHidden);
        photonView.RPC("SyncHiddenVariable", RpcTarget.All, IsHidden);
    }

    private void OnCollisionStay2D(Collision2D collision)       // If local player's tag is Infect, buttonObject is Unactive
    {
        if (collision.collider.CompareTag("Infect") && collision.collider.GetComponent<PhotonView>().IsMine)
        {
            // Debug.Log("Collider Infected");
            CluePanelObject.SetActive(false);
            GameManager.instance.isAlert = false;
        }
    }

    public void ShowCodeCluePanel()
    {
        if (IsHidden)         // 숨겨진 단서면 숨겨진 단서라는 판넬이 뜸
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
        if (IsHidden)         // 숨겨진 단서면 숨겨진 단서라는 판넬이 뜸
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

        yield return new WaitForSeconds(1f);

        GameManager.instance.isAlert = false;
        AlreadyPanelObject.SetActive(false);
    }


    IEnumerator DeactivateHiddenCluePanel()
    {
        yield return new WaitForSeconds(1f);
        HiddenCluePanel.SetActive(false);
        GameManager.instance.isAlert = false;
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
