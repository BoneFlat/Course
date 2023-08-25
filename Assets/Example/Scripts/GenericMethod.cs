namespace Example
{
	using System.Collections.Generic;
	using UnityEngine;

	public class GenericMethod
	{
		public List<Enemy> Enemies;
		public List<Decor> Decors;

		public T FindIndentifierObj<T>(List<T> listObj, int id) where T : IdentifierObj
		{
			return listObj[id];
		}

		public void Execute()
		{
			Enemy enemy1 = FindIndentifierObj<Enemy>(Enemies, 1);
			Decor decor1 = FindIndentifierObj<Decor>(Decors, 1);
		}
	}

	public class IdentifierObj
	{
		public int id;
	}

	public class Enemy : IdentifierObj
	{
		public string name;

		public void DoB()
		{
			
		}
	}

	public class Decor : IdentifierObj
	{
		public Vector2 position;

		public void DoA()
		{
			
		}
	}
}