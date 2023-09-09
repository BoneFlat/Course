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
		[SerializeField] private Text  progress;

		[SerializeField] private float _loadTime = 1f;

		private int  percent = 0;
		private void Start()
		{
			StartCoroutine(DownloadDataAsync());
		}

		private IEnumerator DownloadDataAsync()
		{
			progress.text = "Downloading data...";

			// Dừng quá trình load ở 60%
			float targetProgress = 0.6f;
			float currentProgress = 0f;

			while (currentProgress < targetProgress)
			{
				// Giả lập việc tải dữ liệu
				yield return new WaitForSeconds(0.1f);

				// Tăng tiến trình tải lên
				currentProgress += 0.1f;
				percent = Mathf.Clamp((int)(currentProgress * 100), 0, 100);
				_loadingFill.fillAmount = currentProgress;

				yield return null;
			}

			// Tải xong
			progress.text = "Download success!";
			yield return new WaitForSeconds(1f);

			// Chuyển đến scene ExGame
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
				percent =  Mathf.Clamp((int)(t / _loadTime * 100), 0, 95);
				t       += Time.fixedDeltaTime;

				_loadingFill.fillAmount = t / _loadTime;

				yield return new WaitForFixedUpdate();
			}

			yield return new WaitForSeconds(delaySeconds);
			sceneAsync.allowSceneActivation = true;
		}
	}
}