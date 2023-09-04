using System;
using System.Net.Http;
using UnityEditorInternal;
using UnityEngine;

namespace Example.Scripts.Homework
{
    public class HwAsync : MonoBehaviour
    {
        [SerializeField] private string _uri;
        
        private HttpClient _httpClient;

        private void Start()
        {
            _httpClient = new HttpClient();
        }

        private async void GetStringFromUrl(string uri)
        {
            string response = await _httpClient.GetStringAsync(uri);
        }
    }
}