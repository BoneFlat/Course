using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HWCoordinate : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    public RotateMode _RotateMode;

    public delegate void ButtonClickEvent();
    public event ButtonClickEvent OnButtonClick;

    private GameObject realTarget;
    public enum RotateMode {ByQuaternion, Oz};

    private void Start()
    {
        realTarget = targets[Random.Range(0, targets.Count)];
    }

    private void Update()
    {
        UpdateRotation();
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

    public void HandleButtonClick()
    {
        OnButtonClick = () => { realTarget = targets[Random.Range(0, targets.Count)]; };
        OnButtonClick();
    }
}
