namespace Example
{
	using System;
	using UnityEngine;
	using UnityEngine.Serialization;
    using static UnityEngine.EventSystems.EventTrigger;

	[DefaultExecutionOrder(100000)]
	public class Player : Character
	{
		[FormerlySerializedAs("_maxSpeed")] [Header("Move Settings")] [SerializeField]
		private float _moveSpeed;

		[SerializeField] private float _rotationSpeed;

		[Header("Skill Settings")] [SerializeField]
		private float _lockTimeWhenCast;

		[SerializeField] private float _rotateSpeedWhenCast;

		[SerializeField] private Transform _target;
		[SerializeField] private float     _row = 3;
		[SerializeField] private float     _oxAngle = 50;
		[SerializeField] private float     _distanceBetween = 1;

		private Quaternion _cacheMoveDirection;
		private Vector3    _cachedInput;

        public GameObject bezierProjectilePrefab;
        public GameObject normalProjectilePrefab;
		public GameObject VshapeProjectilePrefab;

		public int numberOfProjectile = 5;

        public KeyCode fireBezierProjectileKey = KeyCode.F;
        public KeyCode fireNormalProjectileKey = KeyCode.G;

        public Transform enemyPosition;

        private void OnEnable()
        {
            if (bezierProjectilePrefab == null || normalProjectilePrefab == null)
            {
                Debug.LogError("Please attach your projectile prefab");
            }
        }

        private void FixedUpdate()
		{
			_cachedInput.x = Input.GetAxisRaw("Horizontal");
			_cachedInput.z = Input.GetAxisRaw("Vertical");
			
			_cachedInput.Normalize();

			transform.position += _moveSpeed * Time.fixedDeltaTime * _cachedInput;

			if (_cachedInput != Vector3.zero)
			{
				var targetRot = Quaternion.LookRotation(_target.transform.position - transform.position, Vector3.up);
				
				transform.rotation =
					Quaternion.RotateTowards(transform.rotation, targetRot, 
						_rotationSpeed * Time.fixedDeltaTime);
			}
		}
        private void Update()
        {
            if (Input.GetKeyDown(fireBezierProjectileKey))
            {
                FireBezierProjectile();
            }
            else if (Input.GetKeyDown(fireNormalProjectileKey))
            {
                FireNormalProjectile();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                FireNormalProjectileInVShape();
            }
        }

        public void StopMove()
		{
			
		}

		public GameSound GameSound;
		public void Die()
		{
            GameSound.DisableSound();
            // GameEventHandler.OnPlayerDie?.Invoke();
		}

		private void OnDrawGizmos()
		{
			var cross = Vector3.Cross(transform.position - _target.position, Vector3.back);
			cross.z = 0;
			var crossAngle = Vector2.SignedAngle(Vector2.right, cross);
			
			Vector3 _deltaPointLeft = Vector3.zero;
			Vector3 _deltaPointRight = Vector3.zero;

			Gizmos.color = Color.red;
			
			for (int i = 1; i < _row; i++)
			{
				_deltaPointRight.x = Mathf.Cos((_oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
				_deltaPointRight.y = Mathf.Sin((_oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;

				_deltaPointLeft.x = Mathf.Cos((180 + crossAngle - _oxAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
				_deltaPointLeft.y = Mathf.Sin((180 - _oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
				
				Gizmos.DrawSphere(transform.position + _deltaPointLeft, 0.1f);
				Gizmos.DrawSphere(transform.position + _deltaPointRight, 0.1f);
			}
			
			Gizmos.DrawSphere(transform.position, 0.1f);
		}

        private void FireBezierProjectile()
        {
            GameObject newProjectile = Instantiate(bezierProjectilePrefab, transform.position, Quaternion.identity);

            // Set the movement type here
            Vector3 playerPosition = transform.position;

            // Example: Bezier movement
            Vector3 controlPoint = (playerPosition + enemyPosition.position) + Vector3.up * 10f;
            newProjectile.GetComponent<BezierProjectile>().SetBezierMovement(playerPosition, controlPoint, enemyPosition.position);
        }

        private void FireNormalProjectile()
        {
            GameObject newProjectile = Instantiate(normalProjectilePrefab, transform.position, Quaternion.identity);

            Vector3 enemyPos = enemyPosition.position; // Set the enemy's position here

            // Example: Straight movement
            newProjectile.GetComponent<NormalProjectile>().SetTarget(enemyPos);
        }

		private void FireNormalProjectileInVShape()
		{

            float angleStep = 180f / (numberOfProjectile - 1); // Angle between each projectile
            float distanceBetweenRows = 1.0f; // Adjust this value to set the vertical distance between rows

            for (int i = 0; i < numberOfProjectile; i++)
            {
                float angle = i * angleStep - 90f; // Angle for the first row of projectiles

                // Calculate the position for the first row
                Vector3 firstRowPosition = transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward;

                // Calculate the position for the second row using reflection
                Vector3 reflection = Vector3.Reflect(firstRowPosition - transform.position, Vector3.up);
                Vector3 secondRowPosition = transform.position + reflection.normalized * distanceBetweenRows;

                // Calculate the direction from the projectiles to the enemy
                Vector3 firstRowDirection = (enemyPosition.position - firstRowPosition).normalized;
                Vector3 secondRowDirection = (enemyPosition.position - secondRowPosition).normalized;

                // Instantiate the projectiles at their respective positions and set their directions
                GameObject firstRowProjectile = Instantiate(VshapeProjectilePrefab, firstRowPosition, Quaternion.identity);
                firstRowProjectile.GetComponent<VShapeProjectile>().SetDirection(firstRowDirection, 2f);

                GameObject secondRowProjectile = Instantiate(VshapeProjectilePrefab, secondRowPosition, Quaternion.identity);
                secondRowProjectile.GetComponent<VShapeProjectile>().SetDirection(secondRowDirection, 2f);
            }


        }

    }
}