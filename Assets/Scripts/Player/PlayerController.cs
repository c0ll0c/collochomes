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
            
        }

        void Update()
        {
            if (!photonView.IsMine || !PhotonNetwork.IsConnected) return;

            // 플레이어 정보 네트워크와 연결
            myInfo = NetworkManager.instance.GetMyStatus();
            Speed = myInfo.Speed;


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

            // moving audio
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

        // detox func
        private void OnCollisionEnter2D(Collision2D collision)
        {
            /*
            if (collision.collider.CompareTag("Detox") && photonView.gameObject.CompareTag("Infect") && photonView.IsMine)
            {
                GameObject effect = transform.Find("effect").gameObject;
                effect.GetComponent<Animator>().runtimeAnimatorController = effectAni;
                effect.SetActive(true);
                StartCoroutine(ResetEffect(effect));

                //this.transform.Find("player trigger").GetComponent<AttackController>().attackActivated = false;
            }
            */
        }

        private IEnumerator ResetEffect(GameObject effect)
        {
            yield return new WaitForSeconds(1.0f);
            effect.SetActive(false);
        }

        void OnMove(InputValue value)
        {
            //inputVec = value.Get<Vector2>();
        }


        [PunRPC]
        void FlipX(float axis)  // 플레이어 이동 시 바라보는 방향 동기화
        {
            spriter.flipX = axis < 0;
        }


        [PunRPC]
        private void EndLoseUI()    // lose 시 UI
        {
            // LoseAudioSourde.clip = LoseAudioCip;
            isAlert = true;
            ending = true;
            UIManager.instance.showLoseUI();
            EscapeAudio.clip = LoseAudio;
            EscapeAudio.Play();
            StartCoroutine(backIntro());
            
        }

        
        private IEnumerator backIntro()     // 엔딩 후 인트로 복귀
        {
            Debug.Log("BACK INTRO START");
            
            yield return new WaitForSeconds(5.0f);
            Debug.Log("BACK INTRO ING");
            NetworkManager.instance.ExitRoom();
            ending = false;
        }
    }
}