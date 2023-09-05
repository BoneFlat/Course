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
        private GameObject bullet, vShapeBullet;
        private float speed = 5f;
        private Vector3 controlPoint;
        private float distance;
        private float startTime;

        private Quaternion _cacheMoveDirection;
        private Vector3 _cachedInput;

        private new void Start()
        {
            base.Start();
            bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            check1 = false;
            check2 = false;

            controlPoint = transform.position + _target.position + Vector3.up * 10;
            distance = Vector3.Distance(transform.position, _target.position);
            startTime = Time.time;

            vShapeBullet = VShapeBullet();
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

        // Tính vị trí trên đường Bézier dựa trên thời gian
        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;

            return p;
        }

        bool check1, check2, check3;
        private void Update()
        {
            //transform.rotation = Quaternion.LookRotation(_target.position - transform.position);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (UnityEngine.Random.Range(0, 3) == 0) // quy dao 1
                {
                    vShapeBullet.SetActive(false);
                    check1 = true;
                    check2 = false;
                    check3 = false;
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                }
                else if (UnityEngine.Random.Range(0, 3) == 1) // quy dao 2
                {
                    vShapeBullet.SetActive(false);
                    check1 = false;
                    check2 = true;
                    check3 = false;
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                }
                else // quy dao 3
                {
                    bullet.SetActive(false);
                    check1 = false;
                    check2 = false;
                    check3 = true;
                    vShapeBullet.transform.position = transform.position;
                    vShapeBullet.SetActive(true);
                }
                
            }
            if (check1)
            {
                bullet.transform.Translate((_target.position - transform.position).normalized * speed * Time.deltaTime);
            }
            if (check2)
            {
                float disCovered = (Time.time - startTime) * speed;
                float percentDis = disCovered / distance;
                //bullet.transform.position = Vector3.Lerp(Vector3.Lerp(transform.position, controlPoint, percentDis),
                //                            Vector3.Lerp(controlPoint, _target.position, percentDis),
                //                            percentDis);
                bullet.transform.position = CalculateBezierPoint(percentDis, transform.position, controlPoint, _target.position);
                if (percentDis >= 1)
                {
                    check2 = false;
                }
            }
            if (check3)
            {
                vShapeBullet.transform.Translate((_target.position - transform.position).normalized * speed * Time.deltaTime);
            }
        }

        private GameObject VShapeBullet()
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