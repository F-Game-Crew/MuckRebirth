using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawNoiseMap (float[,] noiseMap) {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x,y]);
            }
        }
        Texture2D texture = TextureGenerator.FromColorMap(colorMap, height, width);
        ApplyTexture(texture, height, width);
    }

    public void DrawColorMap (float[,] noiseMap, TerrainType[] regions) {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                float currentHeight = noiseMap[x,y];
                foreach(TerrainType region in regions) {
                    if (currentHeight <= region.height) {
                        colorMap[y * width + x] = region.color;
                        break;
                    }
                }
            }
        }
        Texture2D texture = TextureGenerator.FromColorMap(colorMap, height, width);
        ApplyTexture(texture, height, width);
    }

    public void DrawMeshMap (MeshData meshData, float[,] noiseMap, TerrainType[] regions) {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                float currentHeight = noiseMap[x,y];
                foreach(TerrainType region in regions) {
                    if (currentHeight <= region.height) {
                        colorMap[y * width + x] = region.color;
                        break;
                    }
                }
            }
        }

        Texture2D texture = TextureGenerator.FromColorMap(colorMap, height, width);
        Mesh mesh = meshData.CreateMesh();
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
    }

    private void ApplyTexture(Texture2D texture, int height, int width)   {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3 (width, 1, height);
    }
    
}
