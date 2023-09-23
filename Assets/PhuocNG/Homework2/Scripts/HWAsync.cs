using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using UnityEngine;

public class HWAsync : MonoBehaviour
{
    private string contentToSave = "";
    private string filePath = "";

    private async void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "data.txt");

        await TaskOne();
    }

    async Task TaskOne()
    {
        await GetNetString();
    }    

    async Task GetNetString()
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                string url = "https://dotnetfoundation.org";
                await Task.Run(async () =>
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        string content = await httpClient.GetStringAsync(url);
                        contentToSave = content;
                        Debug.Log(content); 

                        await WriteToFileAsync(filePath, contentToSave);
                        Debug.Log("Save File Complete");

                        string fileContent = await ReadFromFileAsync(filePath);
                        Debug.Log("File Content: " + fileContent);
                    }
                });
            }
            catch (HttpRequestException e)
            {
                Debug.Log($"Error HTTP: {e.Message}");
            }
        }
    }

    async Task WriteToFileAsync(string filePath, string content)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            await writer.WriteAsync(content);
        }
    }

    async Task<string> ReadFromFileAsync(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            return await reader.ReadToEndAsync();
        }
    }
}
