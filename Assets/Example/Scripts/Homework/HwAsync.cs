using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HwAsync : MonoBehaviour
{
    private const string StringURL = "https://dotnetfoundation.org";
    private const string StringPath = "Assets/Example/Scripts/Homework/downloadedFile.txt";
    private string _result;
    private string _stringFile;
    
    private void Start()
    {
        RunAnotherThread();
        WriteAndReadFile();
    }

    private async Task LoadStringFromURL()
    {
        var client = new HttpClient();
        try
        {
            _result = await client.GetStringAsync(StringURL);
            Debug.Log($"string from URL: " + _result);
        }
        catch (HttpRequestException e)
        {
            _result = "load error";
            Debug.Log(e);
        }
    }

    private async Task Writer()
    {
        using var writer = new StreamWriter(StringPath);
        await writer.WriteAsync(_result);
        Debug.Log("Save File Complete");
    }

    private async Task<string> Reader()
    {
        using var reader = new StreamReader(StringPath);
        return await reader.ReadToEndAsync();
    }

    private async void WriteAndReadFile()
    {
        await LoadStringFromURL();
        await Writer(); 
        _stringFile = await Reader();
        Debug.Log("--------Read File is complete: " + _stringFile);
    }
    
    private void RunAnotherThread()
    {
        var thread = new Thread(() => LoadStringFromURL());
        thread.Start();
    }
}