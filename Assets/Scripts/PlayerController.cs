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
        public AudioSource EscapeAudio;

        public AudioClip LoseAudio;

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
            EscapeAudio = GetComponent<AudioSource>();
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
            VirusLoseUI = GameObject.Find("게임 기본 UI").transform.Find("VirusLose").gameObject;
            SurvivorLoseUI = GameObject.Find("게임 기본 UI").transform.Find("PlayerLose").gameObject;
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
                StartCoroutine(backIntro());
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

            // 움직임 여부 체크
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
                StartCoroutine(detoxStatus());

                GameObject effect = transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni;
                effect.SetActive(true);
                StartCoroutine(ResetEffect(effect));

                this.transform.Find("player trigger").GetComponent<AttackController>().attackActivated = false;
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
            // LoseAudioSourde.clip = LoseAudioCip;

            myInfo = NetworkManager.instance.GetMyStatus();
            if ((string)myInfo.PlayerStatus == "Virus")
            {
                isAlert = true;
                ending = true;
                VirusLoseUI.SetActive(true);
                EscapeAudio.clip = LoseAudio;
                EscapeAudio.Play();
                StartCoroutine(backIntro());
            }
            else
            {
                isAlert = true;
                ending = true;
                SurvivorLoseUI.SetActive(true);
                // 패배 오디오
                EscapeAudio.clip = LoseAudio;
                EscapeAudio.Play();
                

            }
        }

        private IEnumerator backIntro()
        {
            yield return new WaitForSeconds(5.0f);
            // ending = false;
            NetworkManager.instance.ExitRoom();
        }

        private IEnumerator detoxStatus()
        {
            yield return new WaitForSeconds(1.0f);
            NetworkManager.instance.SetPlayerStatus("Player");
        }
    }
}