using UnityEngine;

public class TextureGenerator
{
   public static Texture2D FromColorMap (Color[] colorMap, int mapHeight, int mapWidth) {
    Texture2D texture = new Texture2D(mapWidth, mapHeight);
    texture.filterMode = FilterMode.Point;
    texture.wrapMode = TextureWrapMode.Clamp;
    texture.SetPixels(colorMap);
    texture.Apply();
    return texture;
   } 
}
