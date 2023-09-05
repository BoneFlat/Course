using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class HwAsync : MonoBehaviour
{
    private const string url = "https://dotnetfoundation.org";
    private const string saveFilePath = "SavedData.txt";

    async void Start()
    {
        // 1. Sử dụng HttpClient để tải nội dung từ URL
        string downloadedString = await DownloadStringAsync(url);

        // 2. Chạy phương thức loading trên một thread khác

        // 3. Ghi nội dung vào tệp và đọc nó sau đó
        await SaveAndReadFileAsync(downloadedString);

        Debug.Log("All tasks completed!");
    }

    async Task<string> DownloadStringAsync(string url)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Tải về dữ liệu từ URL
                string downloadedString = await httpClient.GetStringAsync(url);
                return downloadedString;
            }
            catch (Exception e)
            {
                Debug.LogError("Error downloading data: " + e.Message);
                return string.Empty;
            }
        }
    }

    async Task SaveAndReadFileAsync(string data)
    {
        // Ghi dữ liệu vào tệp
        try
        {
            using (StreamWriter writer = new StreamWriter(saveFilePath))
            {
                await writer.WriteAsync(data);
            }
            Debug.Log("Save File Complete");

            // Đọc nội dung từ tệp và log ra
            using (StreamReader reader = new StreamReader(saveFilePath))
            {
                string fileContent = await reader.ReadToEndAsync();
                Debug.Log("File Content: " + fileContent);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving or reading file: " + e.Message);
        }
    }
}
