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
    }
}
