using UnityEngine;

public class HwDelegate
{
	public delegate void Callback(int x, int y);

	private void Hw1()
	{
		Callback a = (x, y) => { Debug.Log(x + y); };
		Callback b = (x, y) => { Debug.Log(x / y); };
		Callback c = (x, y) => { Debug.Log(x * y); };

		a.Invoke(1, 2);
		b.Invoke(1, 2);
		c.Invoke(1, 2);
	}
}