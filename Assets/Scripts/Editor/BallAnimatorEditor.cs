using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BallAnimatorTool))]
public class BallAnimatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (BallAnimatorTool)target;

        if (GUILayout.Button("Start Enter Tunnel Anim", GUILayout.Height(30)))
        {
            script.StartEnterTunnelAnim();
        }

        if (GUILayout.Button("Start Exit Tunnel Anim", GUILayout.Height(30)))
        {
            script.StartExitTunnelAnim();
        }

        if (GUILayout.Button("Obtain Nodes", GUILayout.Height(30)))
        {
            script.ObtainNodes();
        }

        if (GUILayout.Button("Set Clip", GUILayout.Height(30)))
        {
            script.SetAnimationClips();
        }

        if (GUILayout.Button("Start Recording", GUILayout.Height(30)))
        {
            script.StartRecording();
        }

        if (GUILayout.Button("Stop Recording", GUILayout.Height(30)))
        {
            script.StopRecording();
        }
    }
}
