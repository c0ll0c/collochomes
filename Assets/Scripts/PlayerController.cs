using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

namespace Goldmetal.UndeadSurvivor
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsMoving;

        public AudioSource WalkingSound;

        public Vector2 inputVec;
        public float Speed;
        //public RuntimeAnimatorController[] animCon;
        public bool isAlert = false;
        public bool ending = false;
        [SerializeField] TextMeshProUGUI playername;
        [SerializeField] RuntimeAnimatorController effectAni;

        Rigidbody2D rigid;
        SpriteRenderer spriter;
        Animator anim;
        PhotonView photonView;
        PlayerData myInfo;
        List<PlayerData> playersInfo = new List<PlayerData>();
        GameObject VirusLoseUI;
        GameObject SurvivorLoseUI;

        void Awake()
        {
            photonView = GetComponent<PhotonView>();
            rigid = GetComponent<Rigidbody2D>();
            spriter = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            playername.text = photonView.IsMine ? PhotonNetwork.NickName : photonView.Owner.NickName;   // change player name text

            // camera setting
            if (photonView.IsMine)
            {
                Camera cam = Camera.main;
                cam.transform.SetParent(transform);
                cam.transform.localPosition = new Vector3(0f, 0f, -5f);
            }
        }

        private void Start()
        {
            VirusLoseUI = GameObject.Find("���� �⺻ UI").transform.Find("VirusLose").gameObject;
            SurvivorLoseUI = GameObject.Find("���� �⺻ UI").transform.Find("PlayerLose").gameObject;
            VirusLoseUI.SetActive(false);
            SurvivorLoseUI.SetActive(false);
        }

        void Update()
        {
            if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;

            myInfo = NetworkManager.instance.GetMyStatus();
            Speed = myInfo.Speed;

            if (VirusLoseUI.activeSelf || SurvivorLoseUI.activeSelf)
            {
                ending = true;
            }
        }

        void FixedUpdate()
        {
            if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;
            if (isAlert) return;

            // moving function

            float moveY = Input.GetAxis("Vertical");
            float moveX = Input.GetAxis("Horizontal");
            inputVec = new Vector2(moveX, moveY);

            Vector2 nextVec = inputVec.normalized * Speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);

            // ������ ���� üũ
            IsMoving = inputVec.magnitude > 0.1f;

            if (IsMoving)
            {
                if (!WalkingSound.isPlaying)
                {
                    WalkingSound.Play();
                }
            }
            else
            {
                WalkingSound.Stop();
            }
        }


        void LateUpdate()
        {
            if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;
            if (isAlert) return;

            // animation function
            anim.SetFloat("Speed", inputVec.magnitude);
            if (inputVec.x != 0) photonView.RPC("FlipX", RpcTarget.All, inputVec.x);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Detox") && photonView.gameObject.CompareTag("Infect") && photonView.IsMine)
            {
                Debug.Log("Detox");
                NetworkManager.instance.SetPlayerStatus("Player");
                //photonView.RPC("UpdateInfo", RpcTarget.All);

                GameObject effect = transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni;
                effect.SetActive(true);
                StartCoroutine(ResetEffect(effect));
            }
        }

        private IEnumerator ResetEffect(GameObject effect)
        {
            yield return new WaitForSeconds(1.0f);
            effect.SetActive(false);
        }

        void OnMove(InputValue value)
        {
            inputVec = value.Get<Vector2>();
        }


        [PunRPC]
        void FlipX(float axis)
        {
            spriter.flipX = axis < 0;
        }

        [PunRPC]
        private void InfectRPC()
        {
            NetworkManager.instance.SetPlayerStatus("Infect");
        }

        [PunRPC]
        private void AttackRPC()
        {
            NetworkManager.instance.SetPlayerSpeed(0);
        }

        [PunRPC]
        private void ResetAttackRPC()
        {
            NetworkManager.instance.SetPlayerSpeed(3);
        }

        [PunRPC]
        private void EndLoseUI()
        {
            myInfo = NetworkManager.instance.GetMyStatus();
            if ((string)myInfo.PlayerStatus == "Virus")
            {
                isAlert = true;
                ending = true;
                VirusLoseUI.SetActive(true);
            }
            else
            {
                isAlert = true;
                ending = true;
                SurvivorLoseUI.SetActive(true);
            }
        }

    }
}