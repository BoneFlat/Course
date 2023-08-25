namespace Example
{
	public class ExProperties
	{
		public class Character
		{
			public int _attack;
			
			private int _health;

			public int Health { get; set; }

			public int Power { get { return _attack + _health; } }
		}
	}
}
