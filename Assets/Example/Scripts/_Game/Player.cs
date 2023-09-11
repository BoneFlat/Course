namespace Example
{
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class Player : Character
    {
        [FormerlySerializedAs("_maxSpeed")] [Header("Move Settings")]
        [SerializeField] private float _moveSpeed;

        [SerializeField] private float _rotationSpeed;

        [Header("Skill Settings")]
        [SerializeField] private float _lockTimeWhenCast;

        [SerializeField] private float _rotateSpeedWhenCast;

        [SerializeField] private Transform _target;
        [SerializeField] private float _row = 3;
        [SerializeField] private float _oxAngle = 50;
        [SerializeField] private float _distanceBetween = 1;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int numberBullets = 5;
        private GameObject bullet, shapeBullet;
        private float speed = 1.5f;
        private Vector3 controlPoint;

        private Quaternion _cacheMoveDirection;
        private Vector3 _cachedInput;

        private new void Start()
        {
            base.Start();
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            check1 = false;
            check2 = false;
            check3 = false;

            controlPoint = transform.position + _target.position + Vector3.up * 10;

            shapeBullet = ShapeBullet();
        }

        private void FixedUpdate()
        {
            _cachedInput.x = Input.GetAxisRaw("Horizontal");
            _cachedInput.z = Input.GetAxisRaw("Vertical");

            _cachedInput.Normalize();

            transform.position += _moveSpeed * Time.fixedDeltaTime * _cachedInput;

            if (_cachedInput != Vector3.zero)
            {
                var targetRot = Quaternion.LookRotation(_cachedInput, Vector3.up);

                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRot,
                        _rotationSpeed * Time.fixedDeltaTime);
            }
        }

        //private void OnDrawGizmos()
        //{
        //	var cross = Vector3.Cross(transform.position - _target.position, Vector3.back);
        //	cross.z = 0;
        //	var crossAngle = Vector2.SignedAngle(Vector2.right, cross);

        //	Vector3 _deltaPointLeft = Vector3.zero;
        //	Vector3 _deltaPointRight = Vector3.zero;

        //	Gizmos.color = Color.red;

        //	for (int i = 1; i < _row; i++)
        //	{
        //		_deltaPointRight.x = Mathf.Cos((_oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
        //		_deltaPointRight.y = Mathf.Sin((_oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;

        //		_deltaPointLeft.x = Mathf.Cos((180 + crossAngle - _oxAngle) * Mathf.Deg2Rad) * _distanceBetween * i;
        //		_deltaPointLeft.y = Mathf.Sin((180 - _oxAngle + crossAngle) * Mathf.Deg2Rad) * _distanceBetween * i;

        //		Gizmos.DrawSphere(transform.position + _deltaPointLeft, 0.1f);
        //		Gizmos.DrawSphere(transform.position + _deltaPointRight, 0.1f);
        //	}

        //	Gizmos.DrawSphere(transform.position, 0.1f);
        //}

        bool check1, check2, check3;
        float t = 0;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SpawnProjectile();
            }
        }

        private void SpawnProjectile()
        {
            var index = UnityEngine.Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    shapeBullet.SetActive(false);
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                    bullet.transform.Translate((_target.position - transform.position).normalized * speed * Time.deltaTime);
                    break;
                case 1:
                    shapeBullet.SetActive(false);
                    check1 = false;
                    check2 = true;
                    check3 = false;
                    bullet.transform.position = transform.position;
                    t = 0;
                    bullet.SetActive(true);
                    t += Time.deltaTime;
                    bullet.transform.position = SangExtension.CalculateBezierPoint(t, transform.position, controlPoint, _target.position);
                    break;
                case 2:
                    bullet.SetActive(false);
                    check1 = false;
                    check2 = false;
                    check3 = true;
                    shapeBullet.transform.position = transform.position;
                    shapeBullet.SetActive(true);
                    shapeBullet.transform.Translate((_target.position - transform.position).normalized * speed * Time.deltaTime);
                    break;
            }
        }
        private GameObject ShapeBullet()
        {
            GameObject vShapeBullet = new GameObject();
            vShapeBullet.SetActive(false);
            for (int i = 1; i <= numberBullets; i++)
            {
                Vector3 position;
                if (i % 2 == 0)
                {
                    position = new Vector3(i / 2 * 0.5f, i / 2 * 0.5f, 0);
                }
                else
                {
                    position = new Vector3(-i / 2 * 0.5f, i / 2 * 0.5f, 0);
                }
                GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
                bullet.transform.SetParent(vShapeBullet.transform);
            }
            return vShapeBullet;
        }
    }
}