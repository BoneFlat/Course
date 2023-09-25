using System.Collections;
using UnityEngine;

public class ExGameController : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return StartCoroutine(CountdownCoroutine());
        Debug.Log("Finish Homework");
    }

    private IEnumerator CountdownCoroutine()
    {
        float countdownTime = 10f; // Thời gian đếm ngược là 1 phút
        float elapsedTime = 0f;

        while (elapsedTime < countdownTime)
        {
            yield return new WaitForSecondsRealtime(1f); // Đợi 1 giây thực

            elapsedTime += 1f;

            // Hiển thị thời gian còn lại
            Debug.Log($"Time remaining: {countdownTime - elapsedTime} seconds");
        }
    }
}