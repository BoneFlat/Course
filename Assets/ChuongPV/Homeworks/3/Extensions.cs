namespace ChuongPV
{
	using System.Collections;
	using System.Threading.Tasks;

	public static class Extensions
	{
		public static IEnumerator AsIEnumerator(this Task task)
		{
			while (!task.IsCompleted)
			{
				yield return null;
			}

			if (task.IsFaulted)
			{
				if (task.Exception != null) throw task.Exception;
			}
		}
	}
}