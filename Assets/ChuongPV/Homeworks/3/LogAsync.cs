using System.Threading.Tasks;
using UnityEngine;

public class LogAsync : MonoBehaviour
{
    async void Start()
    {
        await Task.Delay(60 * 1000);
        Debug.Log("Finish Homework");
    }
}
