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
    
    public string changeBGMSceneName = "PlayScene"; // BGM�� ������ ���� �̸��� �����մϴ�.
    public string restartBGMSceneName = "IntroScene"; // BGM�� ������ ���� �̸��� �����մϴ�.
    public AudioClip newBGM; // �ٲ� BGM�� AudioClip�� �����մϴ�.
    public AudioClip initBGM;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        // �� Ŭ������ �ν��Ͻ��� ������ ���� ���� ������Ʈ�� �ν��Ͻ��� �����ϰ�
        // �ı����� �ʵ��� �����մϴ�.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� �ν��Ͻ��� �ִ� ��� ���� ���� ������Ʈ�� �ı��մϴ�.
            Destroy(gameObject);
        }
    }


    private void Start()
    {
    }

    private void Update()
    {
        // ���� ���� �̸��� �����ɴϴ�.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // BGM�� ������ ���� �̸��� ���� ���� �̸��� ������ BGM�� �����մϴ�.
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
