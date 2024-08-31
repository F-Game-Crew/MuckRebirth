using UnityEngine;

public class MeshData 
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int trianglesIndex;

    public MeshData (int meshWidth, int meshHeight) {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangles(int vertex1, int vertex2, int vertex3) {
        triangles[trianglesIndex] = vertex3;
        triangles[trianglesIndex + 1] = vertex2;
        triangles[trianglesIndex + 2] = vertex1;

        trianglesIndex += 3;
    }

    public Mesh CreateMesh () {
        Mesh mesh = new Mesh ();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
