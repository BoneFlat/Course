using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor.VersionControl;
using UnityEditorInternal;
using UnityEngine;
using FileMode = System.IO.FileMode;
using Task = System.Threading.Tasks.Task;

namespace Example.Scripts.Homework
{
    public class HwAsync : MonoBehaviour
    {
        [SerializeField] private string _uri;

        [SerializeField] private string _filePath;

        private HttpClient   _httpClient;

        private FileStream   _fileStream;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;

        private void Start()
        {
            _httpClient   = new HttpClient();

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                LogStringFromUrl(_uri);
                Debug.Log("Get with async");   
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                Thread thread = new Thread(() => LogStringFromUrl(_uri));
                thread.Start();
                Debug.Log("Start get with another thread");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                GetContentFromUriAndWriteToFile(_uri, _filePath);
            }
        }

        private async void LogStringFromUrl(string uri)
        {
            string response = await GetStringFromUrl(uri);
            Debug.Log("Response: " + response);
        }
        
        private async Task<string> GetStringFromUrl(string uri)
        { 
            return await _httpClient.GetStringAsync(uri);
        }

        [Button]
        private async void GetContentFromUriAndWriteToFile(string uri, string path)
        {
            string content = await GetStringFromUrl(uri);
            
            WriteToFile(path, content);
        }

        private async void WriteToFile(string path, string content)
        {
            _fileStream   = new FileStream(path, FileMode.OpenOrCreate);
            _streamWriter = new StreamWriter(_fileStream);
            await _streamWriter.WriteAsync(content);
            await _streamWriter.FlushAsync();
            Debug.Log("Save Complete");
            _fileStream.Close();
        }

        [Button]
        private async void ReadFile(string path)
        {
            _fileStream   = new FileStream(path, FileMode.Open);
            _streamReader = new StreamReader(_fileStream);

            string content = await _streamReader.ReadToEndAsync(); 
            Debug.Log(content);
        }
    }
}