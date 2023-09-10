namespace Jackal
{
	using System;
	using System.Threading;
	using UnityEngine;

	public class ExAsyncAdditional : MonoBehaviour
	{
		private void Start()
		{
			Debug.Log("additional");
		}

		private bool _firstUpdate;
		
		private void Update()
		{
			if (!_firstUpdate)
			{
				Debug.Log("Run Update Additional");
				_firstUpdate = true;
			}
		}
		
		//Ex: Write an async method count from one to ten in 2 second, debug log number every count, then log thread id at the end of method
	}
}