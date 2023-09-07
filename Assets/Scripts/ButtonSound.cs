using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject FalseUI;
    public GameObject TrueUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ButtonOnClick()
    {
        audioSource.Play();
        StartCoroutine(WaitForSoundToFinish());

    }

    private IEnumerator WaitForSoundToFinish()
    {
        // 소리 재생이 끝날 때까지 대기
        yield return new WaitForSeconds(audioSource.clip.length);

        // 소리 재생이 끝나면 버튼을 다시 활성화
        FalseUI.SetActive(false);
        TrueUI.SetActive(true);
    }
}
