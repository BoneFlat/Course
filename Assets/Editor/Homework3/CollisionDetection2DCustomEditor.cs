using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CollisionDetection2D))]
public class CollisionDetection2DCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CollisionDetection2D collisionDetection2D = (CollisionDetection2D)target;

        if (GUILayout.Button("Move to Target"))
        {
            collisionDetection2D.OnMoveToTarget();
        }

        if (GUILayout.Button("Reset position"))
        {
            collisionDetection2D.ResetPosition();
        }
    }
}