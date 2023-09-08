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
    public bool attackActivated = false;
    private float skillCooldownTime = 15.0f;
    private PhotonView photonView;
    private GameObject player;
    [SerializeField]
    private RuntimeAnimatorController[] effectAni;
    private GameObject infectIcon;
    private GameObject attackIcon;
    private GameObject clueIcon;

    private void Start()
    {
        cam = Camera.main;
        photonView = this.GetComponentInParent<PhotonView>();
        player = photonView.gameObject;
        infectIcon = GameObject.Find("game UI").transform.Find("infect").gameObject;
        attackIcon = GameObject.Find("game UI").transform.Find("attack").gameObject;
        clueIcon = GameObject.Find("game UI").transform.Find("clue").gameObject;
    }

    private void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;

        if (player.tag == "Player")
        {
            OnAttackPlayer();
            infectIcon.SetActive(false);
            attackIcon.SetActive(true);
            clueIcon.SetActive(true);
        }
        else
        {
            attackIcon.SetActive(false);
            infectIcon.SetActive(true);
            clueIcon.SetActive(false);
        }

        if (this.GetComponentInParent<PlayerController>().ending)
        {
            infectIcon.SetActive(false);
            attackIcon.SetActive(false);
            clueIcon.SetActive(false);
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
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.Find("attack range").gameObject.SetActive(false);
        }
    }

    private void OnAttackVirus(Collider2D collision)
    {
        if (!photonView.IsMine) return;
        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.transform.Find("attack range").gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonDown(0) && !attackActivated)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);
            Debug.Log("hit : " + hit + "hit.collider : " + hit.collider);
            if (hit && hit.collider == collision && !collision.GetComponentInParent<PhotonView>().IsMine)
            {
                attackActivated = true;

                if (collision.transform.parent.tag == "Player")
                {
                    Debug.Log("infect : " + hit.collider.transform.parent.name);

                    Photon.Realtime.Player targetPlayer = collision.GetComponentInParent<PhotonView>().Owner;
                    StartCoroutine(InfectSkillDelay(targetPlayer));

                    GameObject effect = collision.transform.parent.transform.Find("effect").gameObject;
                    effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[0];
                    effect.SetActive(true);
                    StartCoroutine(ResetEffect(effect));
                }

                infectIcon.GetComponent<CoolTime>().StartCoolTime();
                StartCoroutine(ResetSkillCooldown());
            }
        }
    }

    private void OnAttackPlayer()
    {
        if (Input.GetMouseButtonDown(0) && !attackActivated)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);

            Debug.Log("hit : " + hit + "hit.collider : " + hit.collider);

            if (hit.collider == null) return;
            if (hit.collider.name != "player trigger") return;

            Photon.Realtime.Player targetPlayer = hit.collider.GetComponentInParent<PhotonView>().Owner;
            
            if (hit && !hit.collider.GetComponentInParent<PhotonView>().IsMine)
            {
                attackActivated = true;

                Debug.Log("attack : " + hit.collider.transform.parent.name);
                photonView.RPC("AttackRPC", targetPlayer);

                GameObject effect = hit.collider.transform.parent.transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[1];
                effect.SetActive(true);
                StartCoroutine(ResetEffect(effect));

                attackIcon.GetComponent<CoolTime>().StartCoolTime();
                StartCoroutine(ResetSkillCooldown());
                StartCoroutine(ResetAttackCooldown(targetPlayer));
            }
        }
    }

    private IEnumerator ResetSkillCooldown()
    {
        yield return new WaitForSeconds(skillCooldownTime);
        attackActivated = false;
    }

    private IEnumerator ResetEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1.0f);
        effect.SetActive(false);
    }

    private IEnumerator InfectSkillDelay(Photon.Realtime.Player targetPlayer)
    {
        Debug.Log("Infect START : " + targetPlayer.NickName);
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Infect END : " + targetPlayer.NickName);
        photonView.RPC("InfectRPC", targetPlayer);
    }

    private IEnumerator ResetAttackCooldown(Photon.Realtime.Player targetPlayer)
    {
        yield return new WaitForSeconds(5.0f);
        photonView.RPC("ResetAttackRPC", targetPlayer);
    }

}