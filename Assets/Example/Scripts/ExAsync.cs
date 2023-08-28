using System;
using System.Threading;
using System.IO;
using UnityEditor;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Example.Scripts
{
    
    public class ExAsync : MonoBehaviour
    {
        private async void Start()
        {
            Debug.Log(Thread.CurrentThread.ManagedThreadId);
            
            Task.Run(async () =>
            {
                await Counting1();
                Debug.Log("Task run" + Thread.CurrentThread.ManagedThreadId);
            });
            
            Counting1();
        
           
        }
        
        public async Task Counting1()
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));
            Debug.Log(Thread.CurrentThread.ManagedThreadId);
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
        }
    }
}