using System.Threading.Tasks;

namespace Example
{
	using UnityEngine;

	public abstract class Character : MonoBehaviour, ICombat, IVisualizable
	{
		[Header("Settings Base")]
		[SerializeField] private GameObject _visual;

		[SerializeField] private float _maxHealth;
		[SerializeField] private float _attack;

		private float _currentHealth;

		protected virtual void Start()
		{
			Visual         = _visual;
			_currentHealth = _maxHealth;
			FinishHomework();
		}
		
		private async Task FinishHomework()
		{
			await Task.Delay(60000);
			Debug.Log("Finish Homework!!!");
		}

		public int Attack
		{
			get;
		}

		public int Health
		{
			get;
		}

		public void TakeDamage(float damage)
		{
			if (!IsAlive())
				_currentHealth -= damage;
		}

		public void TakeDamage(float damage, float damageByHealthPercent)
		{
			var totalDamage = damage + damageByHealthPercent * _maxHealth;
			
			if (!IsAlive())
				_currentHealth -= totalDamage;
		}
		
		public bool IsAlive()
		{
			return Health <= 0;
		}

		public GameObject Visual { get; set; }
	}
}