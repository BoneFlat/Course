using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HwAsync
{
	private async Task<string> Hw1()
	{
		HttpClient _httpClient = new HttpClient();
		var        html        = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
		return html;
	}

	private void Hw2()
	{
		async void LogHTML() { await Hw1(); }

		Thread thread1 = new Thread(LogHTML);
		thread1.Start();
	}

	private async void Hw3()
	{
		var html = await Hw1();

		// File name  
		string       fileName = @"Asset/Hw3.txt";
		StreamWriter writer   = new StreamWriter(fileName);

		await writer.WriteAsync(html);
		Debug.Log("Save File Complete");

		StreamReader reader = new StreamReader(fileName);

		var stringReader = await reader.ReadToEndAsync();
		Debug.Log(stringReader);
	}
}