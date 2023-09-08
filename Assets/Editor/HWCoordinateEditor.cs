using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HWCoordinate))]
public class HWCoordinateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HWCoordinate buttonComponent = (HWCoordinate)target;

        if (GUILayout.Button("Pick new Target"))
        {
            buttonComponent.HandleButtonClick();
        }
    }
}
