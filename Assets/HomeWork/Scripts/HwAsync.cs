using UnityEngine;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class HwAsync : MonoBehaviour
{
    private const string url = "https://dotnetfoundation.org";
    private const string filePath = "SavedFile.txt";

    private async void Start()
    {
        await Task.Run(() => DownloadStringAsync());
        await WriteStringToFileAsync();
        await ReadFileContentAsync();
    }

    private async Task DownloadStringAsync()
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync(url);
                Debug.Log("Download Complete");
                Debug.Log("String Length: " + result.Length);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error downloading string: " + e.Message);
        }
    }

    private async Task WriteStringToFileAsync()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(await GetDownloadedStringAsync());
            }
            Debug.Log("Save File Complete");
        }
        catch (Exception e)
        {
            Debug.LogError("Error writing to file: " + e.Message);
        }
    }

    private async Task ReadFileContentAsync()
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = await reader.ReadToEndAsync();
                Debug.Log("File Content: " + content);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading file content: " + e.Message);
        }
    }

    private async Task<string> GetDownloadedStringAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            return await client.GetStringAsync(url);
        }
    }
}