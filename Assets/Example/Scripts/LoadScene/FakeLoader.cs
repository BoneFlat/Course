namespace Jackal
{
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
				percent = Mathf.Clamp((int)(t / _loadTime * 100), 0, 95);
				t += Time.fixedDeltaTime;



				if ((int)percent >= 60)
				{
					downloadingText.gameObject.SetActive(true);
					FetchData();
				}


				_loadingFill.fillAmount = t / _loadTime;

				yield return new WaitForFixedUpdate();
			}

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
}