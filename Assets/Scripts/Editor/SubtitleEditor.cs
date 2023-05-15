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
            script.ReadSpecificLine(1, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 2a", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(2, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 2b", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(2, 2, 3.0f);
        }
        if (GUILayout.Button("Read Line 3a", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(3, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 3b", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(3, 2, 3.0f);
        }
        if (GUILayout.Button("Read Line 4", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(4, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 5a", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(5, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 5b", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(5, 2, 3.0f);
        }
        if (GUILayout.Button("Read Line 5c", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(5, 3, 3.0f);
        }
        if (GUILayout.Button("Read Line 6a", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(6, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 6b", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(6, 2, 3.0f);
        }
        if (GUILayout.Button("Read Line 7a", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(7, 1, 3.0f);
        }
        if (GUILayout.Button("Read Line 7b", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(7, 2, 3.0f);
        }
        if (GUILayout.Button("Read Line 7c", GUILayout.Height(30)))
        {
            script.ReadSpecificLine(7, 3, 3.0f);
        }
    }
}
