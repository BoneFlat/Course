namespace Jackal
{
	using UnityEngine;
	using UnityEngine.SceneManagement;

	public class ExLoadScene : MonoBehaviour
	{
		public void LoadSingle()
		{
			SceneManager.LoadScene("SceneHome", LoadSceneMode.Single);
		}

		public void LoadAdditive()
		{
			SceneManager.LoadScene("SceneHome", LoadSceneMode.Additive);
		}
	}
}