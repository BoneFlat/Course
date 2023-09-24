using Example;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlayerController : MonoBehaviour
{
    private void MoveByDirection(DirectionMove direct)
    {
        var newTransPos = transform.position;
        switch (direct)
        {
            case DirectionMove.Left:
                newTransPos += Vector3.left * .01f;
                break; 
            case DirectionMove.Right:
                newTransPos += Vector3.right * .01f;
                break; 
            case DirectionMove.Up:
                newTransPos += Vector3.up * .01f;
                break;
            case DirectionMove.Down:
                newTransPos += Vector3.down * .01f;
                break;
        }
        if(!CheckHit(newTransPos)) transform.position = newTransPos;
    }

    private bool CheckHit(Vector3 pos)
    {
        var direct = (pos - transform.position).normalized;
        var angle = Mathf.Atan2(direct.x, direct.y);
        var hit = Physics2D.BoxCast(pos, Vector3.one, angle, direct, 2);

        return hit;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) MoveByDirection(DirectionMove.Up);
        if (Input.GetKey(KeyCode.A)) MoveByDirection(DirectionMove.Left);
        if (Input.GetKey(KeyCode.S)) MoveByDirection(DirectionMove.Down);
        if (Input.GetKey(KeyCode.D)) MoveByDirection(DirectionMove.Right);
    }
}

enum DirectionMove
{
    Up, Down, Left, Right
}
