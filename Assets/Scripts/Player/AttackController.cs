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
        if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;

        if (player.tag == "Player")
        {
            CollectionPanelInavtive.IsInfected = false;              
            OnAttackPlayer();
        }

        if (player.tag == "Infect")
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

                if (collision.transform.parent.tag == "Player")
                {
                    Debug.Log("infect : " + hit.collider.transform.parent.name);

                    // 감염
                    Photon.Realtime.Player targetPlayer = collision.GetComponentInParent<PhotonView>().Owner;
                    StartCoroutine(InfectSkillDelay(targetPlayer));     

                    // 효과
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
    }

    private IEnumerator ResetAttackCooldown(Photon.Realtime.Player targetPlayer)    // 공격 초기화
    {
        yield return new WaitForSeconds(5.0f);
        photonView.RPC("ResetAttackRPC", targetPlayer);
    }

}