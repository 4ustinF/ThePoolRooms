using UnityEngine;
using UnityEngine.ProBuilder;

public class VertexCount : MonoBehaviour
{
    public void LogVertexCount()
    {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mesh != null)
        {
            Debug.Log($"MeshFilter Vertex Count: {mesh.vertexCount}");
        }

        ProBuilderMesh proBuilderMesh = GetComponent<ProBuilderMesh>();
        if (proBuilderMesh != null)
        {
            Debug.Log($"ProBuilderMesh Vertex Count: {proBuilderMesh.vertexCount}");
        }
    }

    public void LogTotalVertexCount()
    {
        int count = 0;

        var meshFilters = GameObject.FindObjectsOfType<MeshFilter>();
        foreach (var meshFilter in meshFilters)
        {
            count += meshFilter.sharedMesh.vertexCount;
        }

        var proBuilderMeshs = GameObject.FindObjectsOfType<ProBuilderMesh>();
        foreach (var mesh in proBuilderMeshs)
        {
            count += mesh.vertexCount;
        }

        Debug.Log($"Vertex Count: {count}");
    }
}
