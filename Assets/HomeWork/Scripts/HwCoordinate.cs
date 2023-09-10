using UnityEngine;

public class HwCoordinate : MonoBehaviour
{
    enum RotateMode
    {
        Quaternion,
        NoQuaternion,
    }
    
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private Transform[] targets;
    [SerializeField] private RotateMode mode;
    [SerializeField] private int index = -1;
    
    private void Start()
    {
        index = Random.Range(0, targets.Length);
        var target = targets[index].position;
        switch (mode)
        {
            case RotateMode.Quaternion:
                RotateWithQuaternion(target);
                break;
            case RotateMode.NoQuaternion:
                RotateWithVectorDirection(target);
                break;
        }
    }

    private void RotateWithQuaternion(Vector3 target)
    {
        var direction = (target - mySprite.transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private void RotateWithVectorDirection(Vector3 target)
    {
        var direction = (target - mySprite.transform.position).normalized;
        var angle = Vector3.SignedAngle(Vector3.up, direction, Vector3.forward);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}