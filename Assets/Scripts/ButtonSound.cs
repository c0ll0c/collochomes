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
        // �Ҹ� ����� ���� ������ ���
        yield return new WaitForSeconds(audioSource.clip.length);

        // �Ҹ� ����� ������ ��ư�� �ٽ� Ȱ��ȭ
        FalseUI.SetActive(false);
        TrueUI.SetActive(true);
    }
}
