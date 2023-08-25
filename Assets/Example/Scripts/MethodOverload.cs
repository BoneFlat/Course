namespace Example
{
	public class MethodOverload
	{
		public void TakeDamage(int damage) { }

		public void TakeDamage(int damage, int damageByHealthPercent) { }

		public void TakeDamage(int damage, int damageByHealthPercent, int DOTDamage) { }
	}
}