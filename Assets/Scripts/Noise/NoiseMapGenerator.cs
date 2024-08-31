using UnityEngine;

public class NoiseMapGenerator 
{
    /// <summary>
    ///  POV is top to bottom (2d) <br/>
    ///  + Height is your map length <br/> 
    ///  + Width is your map width </br>
    ///  You can research concepts of this style by following my note </br>
    ///  https://typst.app/project/r5nVND8jNEJbCXcaAu4YuO
    /// </summary>
    /// <returns>Noise map contains noise value in each position (x,y)</returns>
    public static float[,] New (int mapHeight, int mapWidth, float scale, int seed , int octaves_num, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random rand = new System.Random(seed);
        Vector2[] octavesOffset = new Vector2[octaves_num]; 
        for (int i = 0; i < octaves_num; i++) {
            float xOffset = rand.Next(-100000, 100000) + offset.x;
            float yOffset = rand.Next(-100000, 100000) + offset.y;
            octavesOffset[i] = new Vector2(xOffset, yOffset);
        }

        if (scale <= 0) {
            scale = 0.0001f;
        }

        float minNoiseHeight = float.MaxValue;
        float maxNoiseHeight = float.MinValue;
        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float amplitude = 1;        
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves_num; i++) {
                    float calculatedX = (x - halfWidth) / scale * frequency + octavesOffset[i].x;
                    float calculatedY = (y - halfHeight) / scale * frequency + octavesOffset[i].y;
                    
                    float noiseValue = Mathf.PerlinNoise(calculatedX, calculatedY) * 2 - 1; // range [-1;1]
                    noiseHeight += noiseValue * amplitude;

                    frequency *= lacunarity;
                    amplitude *= persistance;
                }

                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                }
                if (minNoiseHeight > noiseHeight) {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x,y] = noiseHeight;
            }
        }

        // Normalize NoiseHeight to range [0;1]
            for (int y = 0; y < mapHeight; y++) {
                for (int x = 0; x < mapWidth; x++) {
                    noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
                }
            }

        return noiseMap; 
    }


}
