using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

//닉네임 입력하는 창
public class EnterRoomUI : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField]
    private TMP_InputField nicknameInputField;
    [SerializeField]
    private GameObject enterRoomUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickEnterGameButton()
    {
        audioSource.Play();
        StartCoroutine(WaitForSoundToFinish());
    }

    // Ready Scene으로 이동
    private IEnumerator WaitForSoundToFinish()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        if (nicknameInputField.text != "")
        {
            SceneManager.LoadScene("ReadyScene");
            NetworkManager.instance.Connect(nicknameInputField.text);
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");   // 닉네임 없을 시 animation
        }
    }
}
