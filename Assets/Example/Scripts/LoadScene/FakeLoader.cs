namespace Jackal
{
	using System.Collections;
    using System.Net.Http;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	public class FakeLoader : MonoBehaviour
	{
		[SerializeField] private Image _loadingFill;
		[SerializeField] private Text  textPercent;
		[SerializeField] private float _loadTime = 1f;
		private int percent = 0;
		private bool isDownloadData;

		private void Start()
		{
			// GameEventHandler.OnLoadGame
			StartCoroutine(LoadSceneAfterWait("ExGame", 1.2f));
		}

		private void Update()
		{
			textPercent.text = (percent + "%");
		}

        public void OnLoadGame()
        {
			Debug.LogError("OnLoadGame");
			HttpClient httpClient = new HttpClient();
			httpClient.GetStringAsync("https://dotnetfoundation.org");
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
				if (percent >= 60 && !isDownloadData)
				{
					isDownloadData = true;
					Time.timeScale = 0;
					OnLoadGame();
				}
				t += Time.fixedDeltaTime;
				_loadingFill.fillAmount = t / _loadTime;
				yield return new WaitForFixedUpdate();
			}

			yield return new WaitForSeconds(delaySeconds);
			sceneAsync.allowSceneActivation = true;
		}
	}
}