using UnityEngine;

public class SlowDownDetoxAnimation : MonoBehaviour
{
    private Animator animator; // �ִϸ����� ������Ʈ

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �ִϸ��̼� �ӵ� ����
        animator.speed = 0.5f; // 0.7 ������� �ִϸ��̼� ���
    }
}
