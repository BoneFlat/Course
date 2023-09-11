using UnityEngine;

public class HwCoordinate : MonoBehaviour
{
    [SerializeField] private Transform mySprite;
    [SerializeField] private Transform[] target;
    private Vector3 prevPos;

    private void Start()
    {
        prevPos = mySprite.position;
    }

    void RotateToTargetByQuaternion(int index)
    {
        Vector3 direction = target[index].position - mySprite.position;
        mySprite.rotation = Quaternion.LookRotation(direction);
    }

    private void RotateToTargetByEulerAngles(int index)
    {
        Vector3 direction = target[index].position - mySprite.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        mySprite.rotation = Quaternion.Euler(0, 0, angle);
    }

    bool isMove;
    float t = 0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RotateToTargetByQuaternion(Random.Range(0, target.Length));
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            RotateToTargetByEulerAngles(Random.Range(0, target.Length));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isMove = true;
        }

        if (isMove)
        {
            t += Time.deltaTime * 1.5f;
            Vector3 newPos = SangExtension.CalculateBezierPoint(t, Vector3.zero, new Vector3(5, 15, 0), new Vector3(10, 0, 0));
            mySprite.position = newPos;

            Vector3 direction = newPos - prevPos;
            prevPos = newPos;
            mySprite.rotation = Quaternion.LookRotation(direction, Vector3.up);
            if (t > 4)
            {
                t = 0;
                mySprite.position = Vector3.zero;
                mySprite.rotation = Quaternion.identity;
                isMove = false;
            }
        }
    }
}