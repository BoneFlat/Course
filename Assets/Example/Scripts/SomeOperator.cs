namespace Example
{
	using System;
	using UnityEngine;

	public class NullOperator
	{
		// ?
		public void Main()
		{
			int? a = null;
			int? b = 5;
			Vector3? c = null;

			// Using the null coalescing operator to provide a default value
			int     result1 = a ?? 0; // result1 is 0
			int     result2 = b ?? 0; // result2 is 5
			Vector3 result3 = c ?? Vector3.zero; // result3 is 0
			
		}

		private ExProperties _exProperties;

		public void Validate()
		{
			_exProperties ??= new ExProperties();
		}
	}

	public class LambdaDemo
	{
		private         string fname = "Thang";
		private         string lname = "Nguyen";
		public override string ToString() => $"{fname} {lname}".Trim();
		
		
		private Action      normalAction;
		
		public void Main()
		{
			normalAction = ExecuteNormal;
			
			void ExecuteNormal()
			{
				Debug.Log("this is normal action");
			}
			
			//same with above
			normalAction = () => Debug.Log("this is normal action");
			normalAction?.Invoke();
		}
		
		// // On class exercise: calculate sum of two interger (x,y)
		// private Action<int, int> powAction;
		// public void MainInput()
		// {
		// 	powAction = ExecutePow;
		// 	void ExecutePow(int a, int b)
		// 	{
		// 		//TODO : calculate sum of 2 interger and log
		// 	}
		// 	
		// 	//use lambda
		// 	powAction = (param) =>
		// 	{
		// 		//TODO : calculate sum of 2 interger and log
		// 	};
		// 	
		// 	powAction?.Invoke(input);
		// }
	}
}