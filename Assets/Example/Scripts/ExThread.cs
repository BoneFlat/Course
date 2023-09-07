namespace Jackal
{
	using System;
	using System.Diagnostics;
	using System.Threading;
	using UnityEngine;
	using Debug = UnityEngine.Debug;

	public class ExThread : MonoBehaviour
	{
		private void Start()
		{
			// Debug.Log($"start in thread {Thread.CurrentThread.ManagedThreadId}");
			
			Thread thread1 = new Thread(DoSomething1);
			thread1.Start();
			thread1.Join(3000);
			
			// Debug.Log($"sleep 3000 ms in thread {Thread.CurrentThread.ManagedThreadId}");
			// Thread.Sleep(3000);
			//
			Thread thread2 = new Thread(DoSomething2);
			thread2.Start();
			thread1.Join(6000);

			ThreadPool.QueueUserWorkItem(DoSomething3);
			
		}
		
		private void DoSomething1()
		{
			Debug.Log($"Screaming 1 in thread {Thread.CurrentThread.ManagedThreadId}");
			Thread.Sleep(2000);
			
			// transform.position += Vector3.left;
			
			Debug.Log($"Screaming 2 in thread {Thread.CurrentThread.ManagedThreadId}");
		}

		private void DoSomething2()
		{
			Thread.Sleep(1000);
			Debug.Log($"Cooking 1 in thread {Thread.CurrentThread.ManagedThreadId}");
			Thread.Sleep(2000);
			
			// transform.position += Vector3.left;
			
			Debug.Log($"Cooking 2 in thread {Thread.CurrentThread.ManagedThreadId}");
		}
		
		private void DoSomething3(object state)
		{
			Debug.Log($"Flying 1 in thread {Thread.CurrentThread.ManagedThreadId}");
			Thread.Sleep(2000);
			
			// transform.position += Vector3.left;
			
			Debug.Log($"Flying 2 in thread {Thread.CurrentThread.ManagedThreadId}");
		}
	}
}