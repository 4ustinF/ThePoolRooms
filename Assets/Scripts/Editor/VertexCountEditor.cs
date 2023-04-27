using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VertexCount))]
public class VertexCountEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (VertexCount)target;

        if (GUILayout.Button("Log Vertex Count", GUILayout.Height(30)))
        {
            script.LogVertexCount();
        }
    }
}
