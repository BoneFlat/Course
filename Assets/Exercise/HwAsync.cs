using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Exercise
{
    public class HwAsync : MonoBehaviour
    {
        private void OnEnable()
        {
            Debug.Log("====================== Exercise 1 ======================");
            Exercise1();
            Debug.Log("====================== Exercise 2 ======================");
            Exercise2();
            Debug.Log("====================== Exercise 3 ======================");
            Exercise3();
        }


        private string _result = "";
        // 1. Dùng HttpClient,getStringAsync để tải string từ url  https://dotnetfoundation.org

        private async Task Exercise1()
        {
            HttpClient client = new HttpClient();
            try
            {
                string url = "https://dotnetfoundation.org";
                _result = await client.GetStringAsync(url);

                Debug.Log(_result);
            }
            catch (HttpRequestException ex)
            {
                Debug.Log($"Download Error: {ex.Message}");
            }
        }
        
        // 2. Chạy phương thức loading trên 1 thread khác
        private async Task Exercise2()
        {
            await Task.Run(Loading);
        }

        private void Loading()
        {
            if (_result != null) return;

            StartCoroutine(CoLoadingString());
        }

        private IEnumerator CoLoadingString()
        {
            var arr = _result.ToArray();

            foreach (var word in arr)
            {
                Debug.Log(word);
                yield return null;
            }
        }
        
        // 3. Viết hàm async sử dụng StreamWritter và StreamReader thực hiện việc sau
        // Write string vừa tải về vào file lưu xong thì log ra "Save File Complete"
        // (đường dẫn tự qui đinh, save file vào đâu cũng được, nếu chạy lại script thì sẽ ghi đè file có sẵn)
        // Read content của file sau khi write xong và log ra
        
        private async Task Exercise3()
        {
            string filePath = "result.txt"; // Đường dẫn đến file lưu trữ

            await Exercise1(filePath);
            await ReadAndLogFile(filePath);
        }
        
        private async Task Exercise1(string filePath)
        {
            HttpClient client = new HttpClient();
            try
            {
                string url = "https://dotnetfoundation.org";
                _result = await client.GetStringAsync(url);

                StreamWriter writer = new StreamWriter(filePath);
                await writer.WriteAsync(_result);
            }
            catch (HttpRequestException ex)
            {
                Debug.Log($"Download Error: {ex.Message}");
            }
        }

        private async Task ReadAndLogFile(string filePath)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);
                string content = await reader.ReadToEndAsync();
                Debug.Log("Nội dung của file:");
                Debug.Log(content);
            }
            catch (IOException ex)
            {
                Debug.Log($"Lỗi đọc file: {ex.Message}");
            }
        }
    }
}