using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Example
{
	public class InnerLoading : MonoBehaviour
	{
		[SerializeField] private Image _loadingFill;
		[SerializeField] private Text  textPercent;
		[SerializeField] private float _loadTime = 1f;

		private int  percent = 0;
		private void Start() { StartCoroutine(LoadSceneAfterWait(GameManager.Instance.sceneToLoad, 1.2f)); }

		private void Update()
		{
			textPercent.text = (percent + "%");
		}

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