using System.Threading.Tasks;

namespace Jackal
{
    using System.Collections;
    using Example;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

<<<<<<< HEAD
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
=======
    public class FakeLoader : MonoBehaviour
    {
        [SerializeField] private Image _loadingFill;
        [SerializeField] private Text textPercent;
        [SerializeField] private float _loadTime = 0.1f;

        private int _percent = 0;
        private int _maxProgress;
>>>>>>> 9c5acb810af42c90f34bfc2cc0a241918509bc62

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

            float t = 0;
            while (t < _loadTime || _maxProgress < 100)
            {
                _percent = Mathf.Clamp((int)(t / _loadTime * 100), 0, _maxProgress);
                t += Time.fixedDeltaTime;

                _loadingFill.fillAmount = (float)_percent / 100;

                await Task.Delay(200);
            }

            _percent = 100;
            _loadingFill.fillAmount = 1;
            await Task.Delay(500);
            
            sceneAsync.allowSceneActivation = true;
        }
    }
}