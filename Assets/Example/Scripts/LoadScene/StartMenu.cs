using UnityEngine;
using UnityEngine.SceneManagement;

namespace Example
{
	public class StartMenu : MonoBehaviour
	{
		public void PlayGame()
		{
			GameManager.Instance.sceneToLoad = "ExGame";
			SceneManager.LoadScene("TempScene");
		}

		public void Reload()
		{
			GameManager.Instance.sceneToLoad = "SceneHome";
			SceneManager.LoadScene("TempScene");
			Time.timeScale = 1;
		}
	}
}