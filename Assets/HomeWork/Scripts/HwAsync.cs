using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class HwAsync : MonoBehaviour
{
    private async Task<string> Crawl(string url)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (HttpRequestException e)
            {
                Debug.Log($"HTTP Request Error: {e.Message}");
            }
        }

        return string.Empty;
    }

    private async void Start()
    {
        var data = await Task.Run(async () => await Crawl("https://dotnetfoundation.org"));
        var path = Application.persistentDataPath + "/hwAsync.txt";
        await SaveData(data, path);
        await LoadData(path);
    }

    private async Task SaveData(string data, string path)
    {
        using (StreamWriter streamWriter = new StreamWriter(File.Open(path, FileMode.Create)))
        {
            await streamWriter.WriteAsync(data);
            Debug.Log("SAVE FILE COMPLETE");
        }
    }

    private async Task LoadData(string path)
    {
        if(!File.Exists(path)) {return;}

        using (StreamReader streamReader = new StreamReader(File.Open(path, FileMode.Open)))
        {
            string data = await streamReader.ReadToEndAsync();
            Debug.Log(data);
        }
    }
}