using UnityEngine;
using UnityEngine.ProBuilder;

public class VertexCount : MonoBehaviour
{
    public void LogVertexCount()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        if (mesh != null)
        {
            Debug.Log($"Mesh Count: {mesh.vertexCount}");
        }

        ProBuilderMesh proBuilderMesh = GetComponent<ProBuilderMesh>();
        if (proBuilderMesh != null)
        {
            Debug.Log($"Probuilder Mesh Count: {proBuilderMesh.vertexCount}");
        }
    }
}
