using UnityEngine;

public class TestStopWatch : MonoBehaviour
{
    const int NUM_LOOP = 10000;
    const int MAX = 1000000;

    private void Start()
    {
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        stopwatch.Start();
        for (int i = 0; i < NUM_LOOP; i++)
        {
            int a = Random.Range(0, MAX);
            int b = Random.Range(0, MAX);
            int s = SumNoBoxing(a, b);
        }
        stopwatch.Stop();
        Debug.LogError("Time with no boxing: " + stopwatch.Elapsed);

        stopwatch.Start();
        for (int i = 0; i < NUM_LOOP; i++)
        {
            int a = Random.Range(0, MAX);
            int b = Random.Range(0, MAX);
            int s = SumWithBoxing(a, b);
        }
        stopwatch.Stop();
        Debug.LogError("Time with boxing: " + stopwatch.Elapsed);
    }

    int SumNoBoxing(int a, int b)
    {
        return a + b;
    }

    int SumWithBoxing(object a, object b)
    {
        return (int)a + (int)b;
    }
}