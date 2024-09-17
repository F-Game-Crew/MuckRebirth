using UnityEngine;

public class MeshGenerator
{
    public static Mesh NewTerrain (float[,] heightMap, AnimationCurve heightCurve, float heightMultipler) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1)/ -2f;
        float topLeftZ = (height - 1)/ 2f;

        MeshData meshData = new MeshData (width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y]) * heightMultipler, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector3(x/(float) width, y/(float) height);

                if (y < height - 1 && x < width - 1) {
                    meshData.AddTriangles(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangles(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData.CreateMesh();
    }    
}

