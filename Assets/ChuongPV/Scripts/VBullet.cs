namespace ChuongPV
{
	using System.Collections.Generic;
	using Example;
	using Sirenix.OdinInspector;
	using UnityEngine;

	public class VBullet : Bullet
	{
		[Header("Config")] [SerializeField] private int       _angle    = 90;
		[SerializeField]                    private int       _nLine    = 2;
		[SerializeField]                    private int       _nInLine  = 3;
		[SerializeField]                    private float     _distance = 5;
		[SerializeField]                    private Transform _childBullet;

		[SerializeField] private List<Transform> _listBullets = new List<Transform>();

		private void Start() { SpawnBullet(); }

		[Button]
		private void SpawnBullet()
		{
			var totalBullets = _nInLine * _nLine + 1;
			InstanceBullets(totalBullets);

			if (_listBullets.Count > 0)
			{
				_listBullets[0].localPosition = Vector3.zero;

				var dirs = new Vector3[_nLine];

				var totalAngle  = _angle * (_nLine - 1);
				var firstRotate = (totalAngle - 180) / 2;

				for (int i = 0; i < _nLine; i++)
				{
					dirs[i] = Vector3.right.RotateBy(firstRotate - _angle * i, 1);
				}

				for (int i = 1; i < totalBullets; i++)
				{
					int column = (i - 1) % _nLine;
					int row    = (i - 1) / _nLine;

					_listBullets[i].localPosition = dirs[column] * (row + 1) * _distance;
				}
			}
		}

		private void InstanceBullets(int totalBullets)
		{
			var count = _listBullets.Count;
			
			if (totalBullets > _listBullets.Count)
			{
				for (int i = 0; i < totalBullets - count; i++)
				{
					var bullet = Instantiate(_childBullet, transform);
					_listBullets.Add(bullet);
				}
			}

			for (int i = 0; i < _listBullets.Count; i++)
			{
				_listBullets[i].gameObject.SetActive(i < totalBullets);
			}
		}
	}
}