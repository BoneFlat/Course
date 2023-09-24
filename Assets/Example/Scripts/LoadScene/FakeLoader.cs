using System.Threading.Tasks;

namespace Jackal
{
	using System.Collections;
	using Example;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;

	

	public class FakeLoader : MonoBehaviour
	{
		[SerializeField] private Image _loadingFill;
		[SerializeField] private Text  textPercent;
		[SerializeField] private float _loadTime = 1f;
		[SerializeField] private Text downloadState;
		private HttpClient https = new HttpClient();
		private string html;
		
		public interface IWeapon
		{
			void GetWeapon();
		}

		public struct Weapon : IWeapon
		{
			public void GetWeapon()
			{
			
			}
		}
		
		public class WeaponClass : IWeapon
		{
			public void GetWeapon()
			{
				
			}
		}

		public void Method()
		{
			var gun = new Weapon();
			var gunClass = new WeaponClass();
			IWeapon weaponClass = gunClass;

			IWeapon weapon = gun;
			IWeapon newGun = (IWeapon)weapon;

			IWeapon newGunClass = (IWeapon)weaponClass;
			
			Debug.Log((newGun));
			Debug.Log((newGunClass));

		}


		private int  percent = 0;
		private async Task Start()
		{
			Method();
			// GameEventHandler.OnLoadGame
			downloadState.text = "WaitForDownload";
			await LoadSceneAfterWait("ExGame");
		}

		private void Update()
		{
			textPercent.text = (percent + "%");
		}

		// public void OnLoadGame(int progress)
		// {
		// 	
		// }

		private async Task LoadSceneAfterWait(string sceneToLoad)
		{
			var sceneAsync = SceneManager.LoadSceneAsync(sceneToLoad);
			sceneAsync.allowSceneActivation = false;
			var downloadData = false;

			float t = 0;
			_loadingFill.fillAmount = 0;
			while (_loadingFill.fillAmount < 1)
			{
				percent =  Mathf.Clamp((int)(t / _loadTime * 100), 0, 101);
				t       += Time.fixedDeltaTime;

				_loadingFill.fillAmount = t / _loadTime;

				if (_loadingFill.fillAmount > .6f && !downloadData)
				{
					_loadingFill.fillAmount = .6f;
					await DownloadData();
					downloadData = true;
					downloadState.text = "Download Complete!!!";
				}

				await Task.Delay(100);
			}
			
			sceneAsync.allowSceneActivation = true;
		}

		private async Task DownloadData()
		{
			downloadState.text = "Downloading...";
			await Task.Run(async () =>
			{
				Debug.Log(1);
				html = await https.GetStringAsync("https://dotnetfoundation.org");
				Debug.Log(html);
			});
		}
		
	}
}