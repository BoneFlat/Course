using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Example;

public class HWCoordinate : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    public RotateMode _RotateMode;

    public delegate void ButtonClickEvent();
    public event ButtonClickEvent OnButtonClick;

    private GameObject realTarget;

    private bool CanBezierMove = false;
    private float t = 0;

    private const float MoveSpeed = 0.1f;
    private Vector3 StartPosition = new Vector3(0, 0, 0);
    private Vector3 Option1Position = new Vector3(5, 15, 0);
    private Vector3 Option2Position = new Vector3(10, 0, 0);

    private Vector3 LastPosition;
    public enum RotateMode {ByQuaternion, Oz};

    private void Start()
    {
        realTarget = targets[Random.Range(0, targets.Count)];
    }

    private void Update()
    {
        UpdateRotation();
        UpdateMoveBezier();
    }

    private void UpdateRotation()
    {
        if(_RotateMode == RotateMode.ByQuaternion)
        {
            // vector from this object towards the target location
            Vector3 vectorToTarget = realTarget.transform.position - gameObject.transform.position;
            // rotate that vector by 90 degrees around the Z axis
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

            // get the rotation that points the Z axis forward, and the Y axis 90 degrees away from the target
            // (resulting in the X axis facing the target)
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            transform.rotation = targetRotation;
        }   
        else if (_RotateMode == RotateMode.Oz)
        {
            Vector3 targetPosition = realTarget.transform.position;
            targetPosition.z = transform.position.z;

            Vector3 direction = targetPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            transform.rotation = rotation;
        }    
    }

    private void UpdateMoveBezier()
    {
        if (!CanBezierMove) return;

        t += Time.fixedDeltaTime * MoveSpeed;
        t = Mathf.Clamp01(t);

        Vector3 newPos = MathfHelper.QuadraticBezier(StartPosition, Option1Position, Option2Position, t);
        LastPosition = transform.position;
        transform.position = newPos;

        Vector3 direction = newPos - LastPosition;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot);

        if (transform.position == Option2Position) CanBezierMove = false;
    }   

    public void HandleButtonClick()
    {
        OnButtonClick = () => { realTarget = targets[Random.Range(0, targets.Count)]; };
        OnButtonClick();
    }

    public void HandleBezierMoveClick()
    {
        transform.position = StartPosition;
        t = 0;
        CanBezierMove = true;
    }    
}
