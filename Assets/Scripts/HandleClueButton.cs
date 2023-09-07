using UnityEngine;
using Photon.Pun;
// �ܼ� ���� ��ư�� �߰� �ϴ� ��ũ��Ʈ

public class HandleClueButton : MonoBehaviour
{
    public string playerTag = "Player";  // Player �±�
    public GameObject clueObject;   // �ܼ� ������Ʈ
    public GameObject buttonObject;   // �ܼ� ���� ��ư

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// �浹 �� ��ư ����

    private void Start()
    {
        buttonObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && collision.collider.GetComponent<PhotonView>().IsMine) // �÷��̾� �±��� ������Ʈ�� �浹 ��
        {
            Debug.Log("clue collision : " + collision.collider.GetComponent<PhotonView>().Owner.NickName);
            buttonObject.SetActive(true); // �ܼ� ���� ��ư�� ���̵��� ����
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // �÷��̾� �±��� ������Ʈ���� �浹�� ������ ��
        {
            buttonObject.SetActive(false); // �ܼ� ���� ��ư�� ���ߵ��� ����
        }
    }

}

// �ܼ��� ������ٰ�, �־����ٰ�, �ٽ� ��������� ���� �۵����� ����
// ó������ �ܼ��� �־��ٰ�, ��������� �� �۵����� ����
// �� isVisible�� false�� �Ǹ� update �Լ��� �� �̻� �۵����� �ʴ� �ɱ�...?
