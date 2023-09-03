using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class AttackController : MonoBehaviour
{
    public AudioSource attackAudio;

    private Camera cam;
    private bool attackActivated = false;
    private float skillCooldownTime = 5.0f;
    private PhotonView photonView;
    private GameObject player;
    [SerializeField]
    private RuntimeAnimatorController[] effectAni;

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
            OnAttackPlayer();
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
            //attackActivated = true;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);

            if (hit && hit.collider == collision && !collision.GetComponentInParent<PhotonView>().IsMine && collision.transform.parent.tag == "Player")
            {
                Debug.Log("infect : " + hit.collider.transform.parent.name);

                Photon.Realtime.Player targetPlayer = collision.GetComponentInParent<PhotonView>().Owner;
                photonView.RPC("InfectRPC", targetPlayer);

                GameObject effect = collision.transform.parent.transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[0];
                effect.SetActive(true);
                StartCoroutine(ResetEffect(effect));


            }

            //StartCoroutine(ResetSkillCooldown());
        }
    }

    private void OnAttackPlayer()
    {
        if (Input.GetMouseButtonDown(0) && !attackActivated)
        {
            //attackActivated = true;

            Vector3 mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, 15f);

            if (hit.collider == null) return;
            if (hit.collider.name != "player trigger") return;

            Photon.Realtime.Player targetPlayer = hit.collider.GetComponentInParent<PhotonView>().Owner;

            if (hit && !hit.collider.GetComponentInParent<PhotonView>().IsMine)
            {
                Debug.Log("attack : " + hit.collider.transform.parent.name);
                photonView.RPC("AttackRPC", targetPlayer);

                GameObject effect = hit.collider.transform.parent.transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni[1];
                effect.SetActive(true);

                attackAudio.Play();

                StartCoroutine(ResetEffect(effect));
                StartCoroutine(ResetAudio(attackAudio));
            }

            //StartCoroutine(ResetSkillCooldown());
            StartCoroutine(ResetAttackCooldown(targetPlayer));
        }
    }

    private IEnumerator ResetSkillCooldown()
    {
        yield return new WaitForSeconds(skillCooldownTime);
        attackActivated = false; // 일정 시간 후에 xSkillActivated를 false로 변경
    }

    private IEnumerator ResetEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1.0f);
        effect.SetActive(false);
    }
    
    private IEnumerator ResetAudio(AudioSource attackAudio)
    {
        yield return new WaitForSeconds(3.0f);
        attackAudio.Stop();
    }



    private IEnumerator InfectSkillDelay(Photon.Realtime.Player targetPlayer)
    {
        yield return new WaitForSeconds(3.0f);
        photonView.RPC("InfecRPC", targetPlayer);
    }

    private IEnumerator ResetAttackCooldown(Photon.Realtime.Player targetPlayer)
    {
        yield return new WaitForSeconds(skillCooldownTime);
        photonView.RPC("ResetAttackRPC", targetPlayer);
    }

}