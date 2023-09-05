using System.Collections.Generic;
using System.Net.Http;

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
		[SerializeField] private Text  textPercent;
		[SerializeField] private float _loadTime = 1f;

		private int        percent = 0;
		private HttpClient _httpClient;
		private List<bool> _checkList;
		
		private void Start()
		{
			// GameEventHandler.OnLoadGame
			_httpClient = new HttpClient();
			_checkList  = new List<bool>();
			_checkList.Add(false);
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

		private async void LoadSomething()
		{
			await _httpClient.GetStringAsync("https://dotnetfoundation.org");
			Time.timeScale = 1;
		}

		private IEnumerator LoadSceneAfterWait(string sceneToLoad, float delaySeconds)
		{
			var sceneAsync = SceneManager.LoadSceneAsync(sceneToLoad);
			sceneAsync.allowSceneActivation = false;

			float t = 0;
			while (t < _loadTime)
			{
				percent =  Mathf.Clamp((int)(t / _loadTime * 100), 0, 95);
				
				if (percent >= 60 && !_checkList[0])
				{
					_checkList[0]  = true;
					Time.timeScale = 0;
					LoadSomething();
				}
				
				t       += Time.fixedDeltaTime;

				_loadingFill.fillAmount = t / _loadTime;

				yield return new WaitForFixedUpdate();
			}

			yield return new WaitForSeconds(delaySeconds);
			sceneAsync.allowSceneActivation = true;
		}
	}
}