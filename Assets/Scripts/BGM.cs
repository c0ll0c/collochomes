using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Goldmetal.UndeadSurvivor;

public class BGM : MonoBehaviour
{
    private static BGM instance;
    
    public string changeBGMSceneName = "PlayScene"; // BGM을 변경할 씬의 이름을 설정합니다.
    public string restartBGMSceneName = "IntroScene"; // BGM을 변경할 씬의 이름을 설정합니다.
    public AudioClip newBGM; // 바꿀 BGM의 AudioClip을 설정합니다.
    public AudioClip initBGM;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // 이 클래스의 인스턴스가 없으면 현재 게임 오브젝트를 인스턴스로 설정하고
        // 파괴되지 않도록 설정합니다.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 있는 경우 현재 게임 오브젝트를 파괴합니다.
            Destroy(gameObject);
        }
    }


    private void Start()
    {
    }

    private void Update()
    {
        // 현재 씬의 이름을 가져옵니다.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // BGM을 변경할 씬의 이름과 현재 씬의 이름이 같으면 BGM을 변경합니다.
        if (currentSceneName == changeBGMSceneName && audioSource.clip != newBGM)
        {
            audioSource.clip = newBGM;
            audioSource.Play();
        }
        
        if (currentSceneName == restartBGMSceneName && audioSource.clip != initBGM)
        {
            audioSource.clip = initBGM;
            audioSource.Play();
        }

        if (GameManager.instance != null && GameManager.instance.gamePlayer != null && GameManager.instance.gamePlayer.GetComponent<PlayerController>().ending)
        {
            audioSource.Stop();
        }
    }
}
