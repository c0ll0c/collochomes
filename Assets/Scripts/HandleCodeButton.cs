using UnityEngine;

// ��ȣ �Է� ��ư�� �߰� �ϴ� ��ũ��Ʈ

public class HandleCodeButton : MonoBehaviour
{
    public string playerTag = "Player";  // Player �±�
    public GameObject portal;   // ���� ������Ʈ
    public GameObject buttonObject;   // ��ȣ �Է� ��ư

    public float maxDistanceToShow = 1.0f; // ���� �Ÿ�

    private bool isVisible = false; // ������Ʈ�� ���ü� ����

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);

        if (playerObject != null)
        {
            float distance = Vector3.Distance(playerObject.transform.position, portal.transform.position);
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
        float distance = Vector3.Distance(playerObject.transform.position, portal.transform.position);

/*        Debug.Log(distance);
        Debug.Log(isVisible);*/

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
}

// �ܼ��� ������ٰ�, �־����ٰ�, �ٽ� ��������� ���� �۵����� ����
// ó������ �ܼ��� �־��ٰ�, ��������� �� �۵����� ����
// �� isVisible�� false�� �Ǹ� update �Լ��� �� �̻� �۵����� �ʴ� �ɱ�...?
