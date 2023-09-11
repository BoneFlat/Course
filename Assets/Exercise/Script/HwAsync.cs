using System.Threading;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System.Net.Http;

public class HwAsync : MonoBehaviour
{
	private async Task<string> Ex1()
	{
		HttpClient httpClient = new HttpClient();
		return await httpClient.GetStringAsync("https://dotnetfoundation.org");
	}

	private void Ex2()
	{
		async void GetString() 
		{ 
			await Ex1(); 
		}
		Thread thread = new Thread(GetString);
		thread.Start();
	}

	private async Task Hw3()
	{
		string path = "Assets/Exercise/ExAsync.txt";

		StreamWriter streamWriter = new StreamWriter(path);
		string stringAsync = await Ex1();
		await streamWriter.WriteAsync(stringAsync);
		Debug.Log("Save File Complete");

		StreamReader streamReader = new StreamReader(path);
		string content = await streamReader.ReadToEndAsync();
		Debug.Log(content);
	}

    private async void Start()
    {
        await CountdownAsync(1);
    }

    static async Task CountdownAsync(int minutes)
	{
		int seconds = minutes * 60;
		while (seconds > 0)
		{
			await Task.Delay(1000);
			seconds--;
		}
		Debug.Log("Finish Homework");
	}
}