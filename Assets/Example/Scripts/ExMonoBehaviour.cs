namespace Example
{
	using System;
	using UnityEngine;

	public class ExMonoBehaviour : MonoBehaviour
	{
		private void Awake()
		{
			Debug.Log($"Awake {Time.frameCount}");
		}

		private void OnEnable()
		{
			Debug.Log($"OnEnable {Time.frameCount}");
		}

		private void Start()
		{
			Debug.Log($"Start {Time.frameCount}");
		}

		private bool _firstUpdate;
		private bool _secondUpdate;

		private void Update()
		{
			if (!_secondUpdate && _firstUpdate)
			{
				_secondUpdate = true;
				Debug.Log($"Second Update {Time.frameCount}");
			}
			
			if (!_firstUpdate)
			{
				_firstUpdate = true;
				Debug.Log($"First Update {Time.frameCount}");
			}
		}

		private bool _firstLateUpdate;
		private bool _secondLateUpdate;
		
		private void LateUpdate()
		{
			if (!_secondLateUpdate && _firstLateUpdate)
			{
				_secondLateUpdate = true;
				Debug.Log($"Second Late Update {Time.frameCount}");
			}
			
			if (!_firstLateUpdate)
			{
				_firstLateUpdate = true;
				Debug.Log($"First Late Update {Time.frameCount}");
			}
		}

		public void DoSomething()
		{
			Debug.Log($"DoSomething {Time.frameCount}");
		}

		private void OnPreRender()
		{
			Debug.Log($"OnPreRender {Time.frameCount}");
		}
	}
}