using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwCoordinate : MonoBehaviour
{
    [SerializeField] private GameObject mySprite;
    [SerializeField] private GameObject[] targets;
    public enum Coord 
    {
        Quaternion, NoQuaternion
    }
    public Coord option;
    // Start is called before the first frame update
    void Start()
    {
        int randomIndex = Random.Range(0, targets.Length);
        GameObject target = targets[randomIndex];

        switch (option)
        {
            case Coord.Quaternion:
                RotateWithQuaternion(target.transform.position);
                break;
            case Coord.NoQuaternion:
                RotateWithVectorRotation(target.transform.position);
                break;
        }
    }

    // Update is called once per frame
    private void RotateWithQuaternion(Vector3 targetPosition)
    {
        // Tính toán vector hướng từ MySprite đến target
        Vector3 direction = (targetPosition - mySprite.transform.position).normalized;

        // Tạo quaternion từ vector hướng
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Áp dụng quaternion để rotate MySprite
        mySprite.transform.rotation = targetRotation;
    }

    private void RotateWithVectorRotation(Vector3 targetPosition)
    {
        // Tính toán vector hướng từ MySprite đến target
        Vector3 direction = (targetPosition - mySprite.transform.position).normalized;

        // Tính góc quay theta từ vector hướng
        float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate MySprite quanh trục Oz (Z-axis) bằng góc theta
        mySprite.transform.rotation = Quaternion.Euler(0, 0, theta);
    }
}
