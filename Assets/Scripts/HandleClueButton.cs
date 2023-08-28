using UnityEngine;

// �ܼ� ���� ��ư�� �߰� �ϴ� ��ũ��Ʈ

public class HandleClueButton : MonoBehaviour
{
    public string playerTag = "Player";  // Player �±�
    public GameObject clueObject;   // �ܼ� ������Ʈ
    public GameObject buttonObject;   // �ܼ� ���� ��ư

    ////////////////////////////////////////////////////////////////////////////////////////////// ��������� ��ư ����

    /*    public float maxDistanceToShow = 1.0f; // ���� �Ÿ�

        private bool isVisible = false; // ������Ʈ�� ���ü� ����

        private void Start()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

            if (playerObject != null)
            {
                float distance = Vector3.Distance(playerObject.transform.position, clueObject.transform.position);
                if (distance <= maxDistanceToShow)
                {
                    isVisible = true;
                    buttonObject.SetActive(true); // ������Ʈ�� ���̵��� ����
                }
                else
                {
                    isVisible = false;
                    buttonObject.SetActive(false); // ������Ʈ�� ���ߵ��� ����
                }
            }
        }

        private void Update()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

            // �÷��̾�� ������Ʈ ������ �Ÿ� ���
            float distance = Vector3.Distance(playerObject.transform.position, clueObject.transform.position);

    *//*        Debug.Log(distance);
            Debug.Log(isVisible);*//*

            // ���� �Ÿ� ���� �÷��̾ �ִٸ� ���ü��� Ȱ��ȭ
            if (distance <= maxDistanceToShow)
            {
               // Debug.Log("�ܼ� �߰�");

               if (!isVisible)             // �� ���̰� �־��ٸ�
                {
                    isVisible = true;
                    buttonObject.SetActive(true); // ������Ʈ�� ���̵��� ����
                }
            }


            // ���� �Ÿ� ���� �÷��̾ ���� �ʴٸ� ���ü��� ��Ȱ��ȭ
            else
            {
               // Debug.Log("�ܼ��κ��� �־���");

                if (isVisible)              // ���̰� �־��ٸ�
                {
                    isVisible = false;
                    buttonObject.SetActive(false); // ������Ʈ�� ���ߵ��� ����
                }

            }

        }
    */

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////// �浹 �� ��ư ����

    private void Start()
    {
        buttonObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) // �÷��̾� �±��� ������Ʈ�� �浹 ��
        {
            Debug.Log("�浹!");
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
