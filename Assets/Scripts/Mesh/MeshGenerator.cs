using UnityEngine;

public class MeshGenerator 
{
    public static MeshData GenerateTerrain (float[,] heightMap, float heightMultipler, AnimationCurve animationCurve, int levelOfDetails) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / 2f;
        float topLeftZ = (height - 1) / 2f;

        // LOD 
        int meshSimplificationIncrement = (levelOfDetails == 0) ? 1 : levelOfDetails * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData (width, height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += meshSimplificationIncrement) {
            for (int x = 0; x < width; x += meshSimplificationIncrement) {
                meshData.vertices[vertexIndex] = new Vector3 (topLeftX - x, animationCurve.Evaluate(heightMap [x,y]) * heightMultipler, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2 (x/(float) width, y/(float) height);

                if (y < height - 1 && x < width - 1) {
                    meshData.AddTriangles(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangles(vertexIndex, vertexIndex + 1, vertexIndex + verticesPerLine + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }
}
