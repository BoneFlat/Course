using System.Net.Http;
using System.Threading.Tasks;

namespace Jackal
{
    using System.Collections;
    using Example;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class FakeLoader : MonoBehaviour
    {
        [SerializeField] private Image _loadingFill;
        [SerializeField] private Text textPercent;
        [SerializeField] private float _loadTime = 1f;
        [SerializeField] private Text textProgress;

        private int percent = 0;

        private void Start()
        {
            // GameEventHandler.OnLoadGame
            StartCoroutine(LoadSceneAfterWait("ExGame", 1.2f));
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

            bool hasDowloaded = false;

            float t = 0;
            while (t < _loadTime)
            {
                percent = Mathf.Clamp((int)(t / _loadTime * 100), 0, 95);
                t += Time.fixedDeltaTime;

                _loadingFill.fillAmount = t / _loadTime;

                if (percent >= 60 && !hasDowloaded)
                {
                    hasDowloaded = true;
                    textProgress.text = "Download Data";
                    var downloadTask = DownloadData();
                    yield return new WaitUntil(() => downloadTask.IsCompleted);
                    textProgress.text = "Download Success";
                    Debug.Log(downloadTask.Result);
                }

                yield return new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(delaySeconds);
            sceneAsync.allowSceneActivation = true;
        }

        private async Task<string> DownloadData()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    Debug.Log("Start download data");
                    HttpResponseMessage response = await httpClient.GetAsync("https://dotnetfoundation.org");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Debug.Log("Download Complete");
                    return content;
                }
                catch (HttpRequestException e)
                {
                    Debug.Log($"HTTP Request Error: {e.Message}");
                }
            }

            return string.Empty;
        }
    }
}