using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaterFallManager))]
public class WaterFallEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (WaterFallManager)target;

        if (GUILayout.Button("Start WaterFall", GUILayout.Height(30)))
        {
            script.StartGameEvent();
        }

        if (GUILayout.Button("Stop WaterFall", GUILayout.Height(30)))
        {
            script.StopGameEvent();
        }
    }
}
