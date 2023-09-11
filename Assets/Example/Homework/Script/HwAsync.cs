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
    private HttpClient https = new HttpClient();

    private string path = "Assets/Example/Homework/homework.txt";

    private string html;
    

    private async void Start()
    {
        // Thread loadThread = new Thread(() =>
        // {
        //     try
        //     {
        //         LoadString();
        //     }
        //     finally
        //     {
        //         WriteAndRead();
        //     } 
        //     
        // });

        // Thread loadThread = new Thread(LoadString);
        //
        // loadThread.Start();
        // loadThread.Join(1);
        
        await Task.Run(async () =>
        {
            Debug.Log(1);
            html = await https.GetStringAsync("https://dotnetfoundation.org");
            Debug.Log(html);
        });
        
        WriteAndRead();

    }

    private async Task WriteAndRead()
    {
        Debug.Log("Run Task");
        var fs = new FileStream(path, FileMode.OpenOrCreate);//Tạo file mới tên là test.txt            
        var sWriter = new StreamWriter(fs, Encoding.UTF8);//fs là 1 FileStream 
        await sWriter.WriteLineAsync(html);
        Debug.Log("Save file complete");
        fs.Close();
        await Read();
    }

    private async Task Read()
    {
        Debug.Log("ReadFile");
        var fs = new FileStream(path, FileMode.Open);//Tạo file mới tên là test.txt            
        var sReader = new StreamReader(fs);
        var line = "1";
        while ((line = await sReader.ReadLineAsync()) != null)
        {
            Debug.Log(line);
        }
    }
    
}
