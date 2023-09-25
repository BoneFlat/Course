using System.Threading.Tasks;

namespace Jackal
{
<<<<<<< HEAD
	using System.Collections;
	using Example;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;
	using System.Threading.Tasks;
	using System.Net.Http;
	using System;

	public class FakeLoader : MonoBehaviour
	{
		[SerializeField] private Image _loadingFill;
		[SerializeField] private Text  textPercent;
		[SerializeField] private float _loadTime = 1f;
		[SerializeField] private Text downloadingText;

		private int  percent = 0;
		private bool stopLoading = false;
		private void Start()
		{
			// GameEventHandler.OnLoadGame
			StartCoroutine(LoadSceneAfterWait("ExGame", 1.2f));
		}
=======
    using System.Collections;
    using Example;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class FakeLoader : MonoBehaviour
    {
        [SerializeField] private Image _loadingFill;
        [SerializeField] private Text textPercent;
        [SerializeField] private float _loadTime = 0.1f;

        private int _percent = 0;
        private int _maxProgress;
>>>>>>> master

        private async void Start()
        {
            GameEventHandler.OnLoadGame += OnLoadGame;
            await LoadSceneAfterWait("ExGame", 0.1f);
        }

        private void OnLoadGame(int maxProgress, LoadGameProgress arg2)
        {
            _maxProgress = maxProgress;
        }
        
        private void Update()
        {
            textPercent.text = (_percent + "%");
        }

        private async Task LoadSceneAfterWait(string sceneToLoad, float delaySeconds)
        {
            var sceneAsync = SceneManager.LoadSceneAsync(sceneToLoad);
            sceneAsync.allowSceneActivation = false;

<<<<<<< HEAD
			float t = 0;
			while (t < _loadTime)
			{
				percent = Mathf.Clamp((int)(t / _loadTime * 100), 0, 95);
				t += Time.fixedDeltaTime;



				if ((int)percent >= 60)
				{
					downloadingText.gameObject.SetActive(true);
					FetchData();
				}

=======
            float t = 0;
            while (t < _loadTime || _maxProgress < 100)
            {
                _percent = Mathf.Clamp((int)(t / _loadTime * 100), 0, _maxProgress);
                t += Time.fixedDeltaTime;
>>>>>>> master

                _loadingFill.fillAmount = (float)_percent / 100;

                await Task.Delay(200);
            }

<<<<<<< HEAD
			yield return new WaitForSeconds(delaySeconds);
			sceneAsync.allowSceneActivation = true;
		}

		private async Task FetchData()
        {
			await DownloadData();
		}			

		private async Task DownloadData()
		{
			string url = "https://dotnetfoundation.org";

			using (HttpClient httpClient = new HttpClient())
			{
				try
				{
					string result = await httpClient.GetStringAsync(url);
					downloadingText.text = "Download Complete!!!";
					Debug.Log(result);
				}
				catch (HttpRequestException e)
				{
					Debug.Log($"Error: {e.Message}");
				}
			}
		}
	}
=======
            _percent = 100;
            _loadingFill.fillAmount = 1;
            await Task.Delay(500);
            
            sceneAsync.allowSceneActivation = true;
        }
    }
>>>>>>> master
}