using System;
using System.Net.Http;
using System.Threading.Tasks;
using Example;
using UnityEngine;

namespace HomeWork
{
    [DefaultExecutionOrder(1000000)]
    public class Downloader : MonoBehaviour
    {
        private async void Start()
        {
            GameEventHandler.OnLoadGame?.Invoke(60, LoadGameProgress.OnDownloadData);
            
            try
            {
                HttpClient _httpClient = new HttpClient();
                _httpClient.Timeout = TimeSpan.FromSeconds(2f);
                var        html        = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
                Debug.Log("Downloaded data as string: " + html);
            }
            catch (HttpRequestException ex)
            {
                
            }

            await Task.Delay(TimeSpan.FromSeconds(1f)); // remove on product, leave it here to see the load process
            GameEventHandler.OnLoadGame?.Invoke(100, LoadGameProgress.OnGameReady);
        }
    }
}