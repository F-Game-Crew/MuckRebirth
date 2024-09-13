using UnityEngine;

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int trianglesIndex;

    public MeshData (int width, int height) {
        vertices = new Vector3[width * height];
        uvs = new Vector2[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
    }

    public void AddTriangles (int vertex1, int vertex2, int vertex3) {
        triangles[trianglesIndex] = vertex1;
        triangles[trianglesIndex + 1] = vertex2;
        triangles[trianglesIndex + 2] = vertex3;

        trianglesIndex += 3;
    } 

    public Mesh CreateMesh () {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

    
}
