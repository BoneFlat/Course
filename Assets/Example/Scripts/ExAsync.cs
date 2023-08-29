using System;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Example.Scripts
{
    using System.Net.Http;
    
    public class ExAsync : MonoBehaviour
    {
        private async void Start()
        {
            // Debug.Log(Thread.CurrentThread.ManagedThreadId);
            // Thread.Sleep(2000);
            // Debug.Log(Thread.CurrentThread.ManagedThreadId);
            //
            //
            // Task.Run(async () =>
            // {
            //     await Counting1();
            //     Debug.Log("Task run" + Thread.CurrentThread.ManagedThreadId);
            // });
            //
            // Counting1();

            WaitCountingInAnotherThread();

            HttpClient _httpClient = new HttpClient();
            var        html        = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
            Debug.Log(html);
            
            // Ex: Write countdown async function trigger OnTimeEnd delegate when timer <= 0;
            // input x = 5s; log time by second

            await CountDown(5, () =>
            {
                Debug.Log("Time End");
            });

        }

        public async Task CountDown(int seconds, Action OnTimeEnd)
        {
            while (seconds > 0)
            {
                Debug.Log(seconds);
                await Task.Delay(TimeSpan.FromSeconds(1));
                seconds--;
            }
            OnTimeEnd?.Invoke();
        }

        public async Task WaitCountingInAnotherThread()
        {
            await Task.Run(async () =>
            {
               await Counting1();
            });
            
            Debug.Log($"Wait counting in {Thread.CurrentThread.ManagedThreadId}");
        }
        
        public async Task Counting1()
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));
            Debug.Log("Counting in " + Thread.CurrentThread.ManagedThreadId);
        }

        public async Task WaitToCounting()
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));
            Debug.Log("start couting");
            await Counting1();
            Debug.Log("stop couting");
        }
        
        public async void LoadAsset()
        {
            var asset = Resources.LoadAsync("path-to-assets");
            
            //write file
            StreamWriter writer = new StreamWriter("path");
            var writeTask = writer.WriteAsync("do write");
            await writeTask;
            
            //read file
            StreamReader reader = new StreamReader("path");
            var readTask = reader.ReadLineAsync();
            string line = await readTask;

            //load asset
            // var loadAsync = 
            // await loadAsync.;
            
            HttpClient _httpClient = new HttpClient();
            var        html        = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
        }
    }
}