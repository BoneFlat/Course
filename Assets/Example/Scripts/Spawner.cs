namespace Example
{
	using UnityEngine;

	public class Spawner : MonoBehaviour
	{
		[SerializeField] private GameObject _prefab;

		public void Spawn()
		{
			var obj = Instantiate(_prefab,transform.position, Quaternion.identity);

			var exMono = obj.GetComponent<ExMonoBehaviour>();
			if (exMono)
				exMono.DoSomething();
			
			// for (int i = 0; i < 5; i++)
			// {
			// 	var obj = Instantiate(_prefab,transform.position, Quaternion.identity);
			//
			// 	var exMono = obj.GetComponent<ExMonoBehaviour>();
			// 	if (exMono)
			// 		exMono.DoSomething();
			// }
		}
	}
}