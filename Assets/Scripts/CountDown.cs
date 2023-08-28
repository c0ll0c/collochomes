using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public float totalTime = 150f; // �� �ð� (�� ����)
    private float currentTime;
    private Text timerText;

    private void Start()
    {
        timerText = GetComponent<Text>();
        currentTime = totalTime;

        UpdateTimerText();
        InvokeRepeating(nameof(UpdateTimer), 1.0f, 1.0f); // 1�ʸ��� Ÿ�̸Ӹ� ����
    }

    private void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime--;
            UpdateTimerText();
        }
        else
        {
            // Ÿ�̸� ���� ó��
            CancelInvoke(nameof(UpdateTimer));
            timerText.text = "Time's up!";
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
