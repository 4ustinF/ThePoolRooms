using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(WaterFallManager))]
public class WaterFallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (WaterFallManager)target;

        if (GUILayout.Button("Start WaterFall", GUILayout.Height(30)))
        {
            script.StartWaterFall();
        }

        if (GUILayout.Button("Stop WaterFall", GUILayout.Height(30)))
        {
            script.StopWaterFall();
        }
    }
}
