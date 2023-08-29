namespace Example
{
	using UnityEngine;

	public static class MethodExtension
	{
		public static Vector3 Plus(this Vector3 root, Vector3 input)
		{
			return root + input;
		}

		public static Vector3 Plus(this Vector3 root, params Vector3[] inputs)
		{
			Vector3 result = root;

			foreach (var input in inputs)
			{
				result += input;
			}

			return result;
		}

		//Excercise: write method return project of vector 3 on oxz plane
		public static Vector3 Plus(this Vector3 root)
		{
			return new Vector3(root.x, 0, root.z);
		}
	}

	public class Test
	{
		public Vector3 test = new Vector3(1, 1, 1);
		public void Test1()
		{
			test.Plus();
		}
	}

	public class DemoMethodExtension
	{
		private Vector3 vectorOne = Vector3.one;
		private Vector3 vectorup = Vector3.up;

		public void Main()
		{
			var sum = vectorOne.Plus(vectorup);
			var sum2 = vectorOne.Plus(vectorup, Vector3.down, Vector3.back);
		}
	}
}