using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HwAsync : MonoBehaviour
{
    async void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetStringAsync();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetStringInAnotherThread();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AsyncWriteFile();
        }
    }

    // 1.
    private async void GetStringAsync()
    {
        HttpClient _httpClient = new HttpClient();
        var task = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
        
        Debug.Log($"Run in {Thread.CurrentThread.ManagedThreadId}, data = {task}");
    }

    // 2.
    private void GetStringInAnotherThread()
    {
        Thread anotherThread = new Thread(GetStringAsync);
        anotherThread.Start();
    }

    // 3.
    private async void AsyncWriteFile()
    {
        string filePath = "Assets/HomeWork/dotnet.txt";
        
        HttpClient _httpClient = new HttpClient();
        string data = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
        
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                await writer.WriteLineAsync(data);
            }
        }
        catch (IOException e)
        {
        }

        if (File.Exists(filePath))
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    stringBuilder.Append(await reader.ReadToEndAsync());
                }
            }
            catch (IOException e)
            {
            }
            
            Debug.Log(stringBuilder.ToString());
        }
    }
}