using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VShapeProjectile : Projectile
{
    private Vector3 direction;
    private float speed = 10f;
    public float destroyTime = 3f; // Thời gian tồn tại của đạn (3 giây trong ví dụ này)

    public void SetDirection(Vector3 dir, float projectileSpeed)
    {
        direction = dir.normalized;
        speed = projectileSpeed;

        // Khi đạn được tạo ra, bắt đầu coroutine để tự hủy sau một khoảng thời gian
        StartCoroutine(DestroyAfterTime());
    }

    void Update()
    {
        // Di chuyển viên đạn theo hướng và tốc độ đã thiết lập
        transform.position += direction * speed * Time.deltaTime;
    }

    private IEnumerator DestroyAfterTime()
    {
        // Đợi cho đến khi đạn tồn tại trong một khoảng thời gian
        yield return new WaitForSeconds(destroyTime);

        // Sau khoảng thời gian đã định, hủy đạn
        Destroy(gameObject);
    }
}
