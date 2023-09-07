namespace Jackal
{
	using System.Collections;
	using System.Net.Http;
	using System.Threading.Tasks;
	using ChuongPV;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	public class FakeLoader : MonoBehaviour
	{
		[SerializeField] private Image _loadingFill;
		[SerializeField] private Text  textPercent;
		[SerializeField] private Text  textLoadData;
		[SerializeField] private float _loadTime = 1f;

		private int  percent    = 0;
		private bool isLoading  = true;
		private bool loadedData = false;

		private void Start()
		{
			// GameEventHandler.OnLoadGame
			StartCoroutine(LoadSceneAfterWait("ExGame", 1.2f));
		}

		private void Update() { textPercent.text = (percent + "%"); }

		// public void OnLoadGame(int progress)
		// {
		// 	
		// }

		private async Task<string> Hw1()
		{
			HttpClient _httpClient = new HttpClient();
			var        html        = await _httpClient.GetStringAsync("https://dotnetfoundation.org");
			return html;
		}

		private async Task LoadData()
		{
			isLoading         = false;
			textLoadData.text = "Loading Data";
			var html = await Hw1();
			Debug.LogWarning(html);
			textLoadData.text = "Load Data Success";
			loadedData        = true;
			isLoading         = true;
		}

		// ReSharper disable Unity.PerformanceAnalysis
		private IEnumerator LoadSceneAfterWait(string sceneToLoad, float delaySeconds)
		{
			var sceneAsync = SceneManager.LoadSceneAsync(sceneToLoad);
			sceneAsync.allowSceneActivation = false;

			float t = 0;

			while (t < _loadTime)
			{
				if (isLoading)
				{
					t += Time.fixedDeltaTime;

					if (t >= 0.6f && !loadedData)
					{
						Debug.Log("load data");
						yield return LoadData().AsIEnumerator();
					}

					percent                 = Mathf.Clamp((int) (t / _loadTime * 100), 0, 95);
					_loadingFill.fillAmount = t / _loadTime;

					yield return new WaitForFixedUpdate();
				}
			}

			yield return new WaitForSeconds(delaySeconds);
			sceneAsync.allowSceneActivation = true;
		}
	}
}