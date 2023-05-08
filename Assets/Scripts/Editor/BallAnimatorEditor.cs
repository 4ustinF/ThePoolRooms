using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BallAnimatorTool))]
public class BallAnimatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (BallAnimatorTool)target;

        if (GUILayout.Button("Start Anim", GUILayout.Height(30)))
        {
            script.StartAnim();
        }

        if (GUILayout.Button("Obtain Nodes", GUILayout.Height(30)))
        {
            script.ObtainNodes();
        }
    }
}
