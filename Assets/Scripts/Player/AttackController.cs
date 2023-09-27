using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using Goldmetal.UndeadSurvivor;

public class AttackController : MonoBehaviour
{
    private Camera cam;
    public bool attackActivated = false;    // attack cool time
    private float skillCooldownTime = 15.0f;

    private PhotonView photonView;
    private GameObject player;

    static public bool IsAudioPlay = false;
    
    [SerializeField] private RuntimeAnimatorController[] effectAni;

    private void Start()
    {
        cam = Camera.main;
        photonView = this.GetComponentInParent<PhotonView>();
        player = photonView.gameObject;
    }

    private void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();

        if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;

        if (player.tag == "Player")
        {
            CollectionPanelInavtive.IsInfected = false;              
            OnAttackPlayer();
        }

        if (player.tag == "Infect")                 // 지금 켜져 있는 판넬들 다 꺼지고, IsVaccinated도 무효화
        {
            CollectionPanelInavtive.IsInfected = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.tag == "Virus" || player.tag == "Infect")
        {
            OnAttackVirus(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)    // collider가 player인지
        {
            collision.gameObject.transform.Find("attack range").gameObject.SetActive(false);
        }
    }

    // Infect method
    private void OnAttackVirus(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        
        // 공격 범위 표시
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.Find("attack range").gameObject.SetActive(true);
        }

        // 클릭 & 쿨타임
        if (Input.GetMouseButtonDown(0) && !attackActivated)
        {
            // 마우스로 클릭한 오브젝트 받아오기
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);
            Debug.Log("hit : " + hit + "hit.collider : " + hit.collider);

            // 클릭한 오브젝트가 공격 범위 내 오브젝트와 동일한지 확인 (자신 제외)
            if (hit && hit.collider == collision && !collision.GetComponentInParent<PhotonView>().IsMine)
            {
                attackActivated = true;     // 쿨타임 시작

                if (collision.transform.parent.tag == "Player")             // 클릭한 오브젝트의 태그가 플레이어면
                {
                    Photon.Realtime.Player targetPlayer = collision.GetComponentInParent<PhotonView>().Owner;
                    Debug.Log((string)targetPlayer.CustomProperties["Nickname"] + ": 공격 상대");

                    StartCoroutine(VaccinatedOrNot(targetPlayer));          // 백신 접종이 됐냐 안 됐냐

                    Debug.Log("infect : " + hit.collider.transform.parent.name);

                    // 효과           // 백신 접종돼 있어도 효과는 나게
                    GameObject effect = collision.transform.parent.transform.Find("effect").gameObject;
                    effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[0];
                    effect.SetActive(true);
                    StartCoroutine(ResetEffect(effect));
                }

                UIManager.instance.startCoolTime();
                StartCoroutine(ResetSkillCooldown());   // 15초 뒤 쿨타임 초기화
            }
        }
    }

    private IEnumerator VaccinatedOrNot(Photon.Realtime.Player targetPlayer)
    {
        yield return new WaitForSeconds(2.0f);

        if (targetPlayer.CustomProperties["Vaccinated"] == null)            // 맨 처음에 정의가 안 됨, 왜??
        {
            Debug.Log("Virus: 감염");
            StartCoroutine(InfectSkillDelay(targetPlayer));
        }
        else if ((bool)targetPlayer.CustomProperties["Vaccinated"])              // 상대 플레이어가 백신 접종이 됐으면...
        {
            Debug.Log("Virus: 백신 효과 해제");
            StartCoroutine(UnVaccinatedDelay(targetPlayer));                       // "상대 플레이어"가 백신 접종돼 있고, 감염 공격을 받았으면 백신 효과 해제
        }
        else                        // "상대 플레이어"가 백신 접종 X -> 감염 -> 접종 O(1초도 안 걸림)이면... 
        {
            Debug.Log("Virus: 감염");
            StartCoroutine(InfectSkillDelay(targetPlayer));
        }
    }

    // Attack method
    private void OnAttackPlayer()
    {
        // 클릭 & 쿨타임
        if (Input.GetMouseButtonDown(0) && !attackActivated)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);
            Debug.Log("hit : " + hit + "hit.collider : " + hit.collider);

            if (hit.collider == null) return;
            if (hit.collider.name != "player trigger") return;

            Photon.Realtime.Player targetPlayer = hit.collider.GetComponentInParent<PhotonView>().Owner;    // 공격할 플레이어 정보 받아옴
            
            if (hit && !hit.collider.GetComponentInParent<PhotonView>().IsMine)
            {
                attackActivated = true;     // 쿨타임 시작

                // 공격
                Debug.Log("attack : " + hit.collider.transform.parent.name);
                photonView.RPC("AttackRPC", targetPlayer);

                // 효과
                GameObject effect = hit.collider.transform.parent.transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[1];
                effect.SetActive(true);
                StartCoroutine(ResetEffect(effect));

                UIManager.instance.startCoolTime();
                StartCoroutine(ResetSkillCooldown());   // 쿨타임 시작
                StartCoroutine(ResetAttackCooldown(targetPlayer));  // 공격 타이머 시작
            }
        }
    }

    private IEnumerator ResetSkillCooldown()        // 쿨타임 초기화
    {
        yield return new WaitForSeconds(skillCooldownTime);
        attackActivated = false; 
    }

    private IEnumerator ResetEffect(GameObject effect)      // 효과 애니메이션 초기화
    {
        yield return new WaitForSeconds(1.0f);
        effect.SetActive(false);
    }

    private IEnumerator InfectSkillDelay(Photon.Realtime.Player targetPlayer)   // 감염 연기
    {
        yield return new WaitForSeconds(5.0f);
        photonView.RPC("InfectRPC", targetPlayer);
        if (targetPlayer.CustomProperties.ContainsKey("Vaccinated"))              // 백신 접종 유무가 정의가 된 상태이고 (한번 이상 백신 해제)
        {
            if ((bool)targetPlayer.CustomProperties["Vaccinated"])              // 상대 플레이어가 백신 접종이 됐으면
                photonView.RPC("UnvaccineRPC", targetPlayer);
        }
    }

    private IEnumerator UnVaccinatedDelay(Photon.Realtime.Player targetPlayer)   // 백신 해제 연기
    {
        yield return new WaitForSeconds(5.0f);
        photonView.RPC("UnvaccineRPC", targetPlayer);
    }

    private IEnumerator ResetAttackCooldown(Photon.Realtime.Player targetPlayer)    // 공격 초기화
    {
        yield return new WaitForSeconds(5.0f);
        photonView.RPC("ResetAttackRPC", targetPlayer);
    }

}