using UnityEngine;

public class SlowDownDetoxAnimation : MonoBehaviour
{
    private Animator animator; // 애니메이터 컴포넌트

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // 애니메이션 속도 조절
        animator.speed = 0.5f; // 0.7 배속으로 애니메이션 재생
    }
}
