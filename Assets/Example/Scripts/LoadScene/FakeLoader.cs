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
        [SerializeField] private float _loadTime = 0.1f;

        private int _percent = 0;
        private int _maxProgress;

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