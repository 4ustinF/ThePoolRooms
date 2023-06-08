using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FishManager))]
public class FishEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (FishManager)target;

        if (GUILayout.Button("Start Fish", GUILayout.Height(30)))
        {
            script.StartFishParticles();
        }

        if (GUILayout.Button("Stop Fish", GUILayout.Height(30)))
        {
            script.StopFishParticles();
        }
    }
}
