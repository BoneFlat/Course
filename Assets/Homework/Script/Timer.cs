using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;

    [SerializeField] float totalTime = 60f;

    private async void Start()
    {
        await CountdownTimer();
    }

    private async Task CountdownTimer()
    {
        while (totalTime > 0)
        {
            UpdateTimerDisplay(totalTime);
            await Task.Delay(TimeSpan.FromSeconds(1.0)); // Wait for 1 second asynchronously
            totalTime -= 1.0f; // Giảm thời gian đếm ngược
        }

        // Khi hết thời gian, log "Finish Homework"
        Debug.Log("Finish Homework");
    }

    private void UpdateTimerDisplay(float time)
    {
        // Hiển thị thời gian đếm ngược trong định dạng phút:giây
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = timeString;
    }
}
