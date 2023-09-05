namespace Jackal
{
    using UnityEngine;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using System.Collections;

    public class FakeLoader : MonoBehaviour
	{
		[SerializeField] private Image _loadingFill;
		[SerializeField] private Text  textPercent;
        [SerializeField] private Text textDownloadStatus;
        [SerializeField] private float _loadTime = 1f;

		private float  percent = 0;
		private async void Start()
		{
            // GameEventHandler.OnLoadGame
            // StartCoroutine(LoadSceneAfterWait("ExGame", 1.2f));

            // Dừng quá trình load ở 60%
            await LoadFakeProgress(0.6f);

            // Hiển thị "download data" và thực hiện tải data
            textDownloadStatus.text = "Downloading data...";
            string downloadedData = await DownloadDataAsync("https://dotnetfoundation.org");

            // Hiển thị "download success"
            textDownloadStatus.text = "Download success!";

            Debug.Log(downloadedData);

            // Load vào scene exgame sau khi hoàn thành quá trình load
            SceneManager.LoadScene("ExGame");
        }

		private void Update()
		{
			textPercent.text = (percent + "%");
		}

		// public void OnLoadGame(int progress)
		// {
		// 	
		// }

		private IEnumerator LoadSceneAfterWait(string sceneToLoad, float delaySeconds)
		{
			var sceneAsync = SceneManager.LoadSceneAsync(sceneToLoad);
			sceneAsync.allowSceneActivation = false;

			float t = 0;
			while (t < _loadTime)
			{
				percent =  Mathf.Clamp((int)(t / _loadTime * 100), 0, 95);
				t       += Time.fixedDeltaTime;

				_loadingFill.fillAmount = t / _loadTime;

                yield return new WaitForFixedUpdate();
                
			}

            yield return new WaitForSeconds(delaySeconds);
            
			sceneAsync.allowSceneActivation = true;
		}
        private async Task LoadFakeProgress(float targetProgress)
        {
            float currentProgress = 0f;
            while (currentProgress < targetProgress)
            {
                //currentProgress += Time.deltaTime * 0.2f; // Thay đổi tốc độ tải tùy ý
                //percent = currentProgress;
                percent = Mathf.Clamp((int)(currentProgress / _loadTime * 100), 0, 95);
                currentProgress += Time.fixedDeltaTime;

                _loadingFill.fillAmount = currentProgress / _loadTime;
                await Task.Yield();
            }

            await Task.Delay(TimeSpan.FromSeconds(targetProgress));
        }

        private async Task<string> DownloadDataAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Tải dữ liệu từ URL
                    string downloadedData = await httpClient.GetStringAsync(url);
                    return downloadedData;
                }
                catch (Exception e)
                {
                    Debug.LogError("Error downloading data: " + e.Message);
                    return string.Empty;
                }
            }
        }
    }
}