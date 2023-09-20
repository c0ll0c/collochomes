using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����ó�� ���� ��ư ������ �ܼ� ������ â�� �߰� �ϴ� ��ũ��Ʈ
// ���� ��ư�� �Ҵ��� �ֱ�

public class GameUIClue : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject button;       // ����ó�� ���� ��ư
    public GameObject CollectionPanel;        // �ܼ� ������ �ǳ�

    // Start is called before the first frame update
    void Start()
    {
        CollectionPanel.SetActive(false);
    }

    public void ShowCollection()
    {
        GameManager.instance.isAlert = true;
        CollectionPanel.SetActive(true);            // ��ư ������ �ܼ� ������ �ǳ� ���ü��� ���̰� �ϱ�
        // isAlert = true�� �ؼ� ĳ���� �� �����̰� �� ��
    }

    public void CloseCollection()
    {
        audioSource.Play();
        StartCoroutine(WaitForSoundToClose());
    }

    private IEnumerator WaitForSoundToClose()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        GameManager.instance.isAlert = false;
        CollectionPanel.SetActive(false);
    }

}
