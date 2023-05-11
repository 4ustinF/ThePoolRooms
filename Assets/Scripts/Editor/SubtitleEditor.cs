using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SubtitleManager))]
public class SubtitleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (SubtitleManager)target;

        if (GUILayout.Button("Read Next Line", GUILayout.Height(30)))
        {
            script.ReadNextLine();
        }
        if (GUILayout.Button("Read Line 1", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(1);
        }
        if (GUILayout.Button("Read Line 2", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(2);
        }
        if (GUILayout.Button("Read Line 3", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(3);
        }
        if (GUILayout.Button("Read Line 4", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(4);
        }
        if (GUILayout.Button("Read Line 5", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(5);
        }
        if (GUILayout.Button("Read Line 6", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(6);
        }
        if (GUILayout.Button("Read Line 7", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(7);
        }
    }
}
