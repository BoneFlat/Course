using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class HwCoordinate : MonoBehaviour
{
    public GameObject mySprite;
    public GameObject[] targets;
    public float rotationSpeed = 2.0f;

    public Transform[] controlPoints; // Các điểm kiểm soát Bézier (0,0,0), (5,15,0), (10,0,0)
    public float movementSpeed = 1.0f; // Tốc độ di chuyển
    private float t = 0f; // Tham số t của đường cong Bézier
    private Vector3 lastPosition; // Vị trí trước đó của hình chữ nhật
    private Vector3 direction; // Hướng pháp tuyến của đường cong Bézier


    private void Start()
    {
        // Gán vị trí ban đầu của hình chữ nhật
        transform.position = controlPoints[0].position;

        // Lưu vị trí ban đầu và tính hướng pháp tuyến của đường cong Bézier
        lastPosition = transform.position;
        direction = CalculateBezierDerivative(t);
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
        // Lựa chọn ngẫu nhiên một trong bốn "Target"
        GameObject target = targets[Random.Range(0, targets.Length)];

        // Cách 1: Sử dụng Quaternion để xoay "MySprite" đến "Target"
        RotateUsingQuaternion(mySprite.transform, target.transform);

        // Cách 2: Sử dụng phép quay vector
         //RotateUsingVector(mySprite.transform, target.transform);
        }
        */

        // Tăng tham số t dựa trên tốc độ di chuyển
        t += Time.deltaTime * movementSpeed;

        // Di chuyển hình chữ nhật theo đường cong Bézier
        transform.position = CalculateBezierPoint(t);

        // Tính hướng pháp tuyến mới và xoay hình chữ nhật
        Vector3 newPosition = transform.position;
        direction = (newPosition - lastPosition).normalized;
        lastPosition = newPosition;

        // Xoay hình chữ nhật để hướng foward luôn theo hướng pháp tuyến của đường cong
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void RotateUsingQuaternion(Transform start, Transform target)
    {
        Vector3 direction = target.position - start.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        RotateObject(start, rotation);
    }

    private async void RotateObject(Transform objectTransform, Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion initialRotation = objectTransform.rotation;

        while (elapsedTime < 1f)
        {
            objectTransform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            await Task.Yield();
        }
    }

    // Cách 2: Sử dụng phép quay vector
    private void RotateUsingVector(Transform start, Transform target)
    {
        Vector3 direction = target.position - start.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        start.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private Vector3 CalculateBezierPoint(float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * controlPoints[0].position; // (1-t)^3 * P0
        p += 3 * uu * t * controlPoints[1].position; // 3(1-t)^2 * t * P1
        p += 3 * u * tt * controlPoints[2].position; // 3(1-t) * t^2 * P2
        p += ttt * controlPoints[2].position; // t^3 * P2

        return p;
    }

    private Vector3 CalculateBezierDerivative(float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 dP = -3 * uu * controlPoints[0].position;
        dP += (3 * uu - 6 * u * t) * controlPoints[1].position;
        dP += (-3 * tt + 6 * u * t) * controlPoints[2].position;
        dP += 3 * tt * controlPoints[2].position;

        return dP.normalized;
    }
}
