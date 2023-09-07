using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class MySprite : MonoBehaviour
{
	[SerializeField] private Corner[] _corners;

	[Button]
	private void RotateRandom1()
	{
		var rand = Random.Range(0, 4);
		Debug.Log($"Way1 \nRotate to corner : {(CornerType) rand}");
		RotateToCorner1((CornerType) rand);
	}

	[Button]
	private void RotateRandom2()
	{
		var rand = Random.Range(0, 4);
		Debug.Log($"Way2 \nRotate to corner : {(CornerType) rand}");
		RotateToCorner2((CornerType) rand);
	}

	private void RotateToCorner1(CornerType cornerType)
	{
		var corner = GetCornerWithType(cornerType);
		Debug.Log($"Corner : {corner.name}");
		transform.rotation = Quaternion.LookRotation(corner.transform.position - transform.position);
	}

	private void RotateToCorner2(CornerType cornerType)
	{
		var corner = GetCornerWithType(cornerType);
		Debug.Log($"Corner : {corner.name}");
		transform.rotation = Quaternion.LookRotation(corner.transform.position - transform.position);
	}

	private Corner GetCornerWithType(CornerType cornerType) { return _corners.ToList().Find(corner => corner.CornerType == cornerType); }
}