using UnityEngine;

public class SlowDownAnimation : MonoBehaviour
{
    private Animator animator; // �ִϸ����� ������Ʈ
    public float Speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �ִϸ��̼� �ӵ� ����
        // animator.speed = 0.5f; // 0.7 ������� �ִϸ��̼� ���
        animator.speed = Speed; // 0.7 ������� �ִϸ��̼� ���
    }
}
