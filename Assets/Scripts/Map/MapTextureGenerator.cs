using UnityEngine;

public class MapTextureGenerator
{
   public static Texture2D CreateTexture2DFromColorMap (Color[] colorMap, int width, int height) {
    Texture2D texture = new Texture2D(width, height);
    texture.filterMode = FilterMode.Point;
    texture.SetPixels(colorMap);
    texture.Apply();
    return texture;
   }

    public static Texture2D NoiseFromHeightMap (float[,] heightMap) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x,y]);
            }
        }

        return CreateTexture2DFromColorMap(colorMap, width, height);
;
    }

    public static Texture2D ColorFromHeightMap (float[,] heightMap, TerrainType[] regions) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(0);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                float currentHeight = heightMap[x,y];
                foreach (TerrainType region in regions) {
                    if (currentHeight < region.height) {
                        colorMap[y * width + x] = region.color;
                        break;
                    }
                }
            }
        }

        return CreateTexture2DFromColorMap(colorMap, width, height);
    }
}
