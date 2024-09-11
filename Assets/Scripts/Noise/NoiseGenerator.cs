using System.Linq;
using UnityEngine;

public class NoiseGenerator
{
   public static float[,] NewTerrain (int width, int height, int seed, float noiseScale, int octaves, float lacunarity, float persistance, Vector2 offset) {
    float[,] noiseMap = new float[width, height];

    System.Random rand = new System.Random(seed);
    Vector2[] octavesOffset = new Vector2[octaves]; 
    for (int i = 0; i < octaves; i++) {
        float xOffset = rand.Next(-100000, 100000) + offset.x;
        float yOffset = rand.Next(-100000, 100000) + offset.y;
        octavesOffset[i] = new Vector2(xOffset, yOffset);
    }

    float minValue = float.MaxValue;
    float maxValue = float.MinValue;

    float halfWidth = width / 2;
    float halfHeight = height / 2;

    for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {
            float amplitudes = 1;
            float frequency = 1;
            float heightValue = 1;

            for (int i = 0; i < octaves; i++) {
                float calculatedX = (float) (x - halfWidth) / noiseScale * frequency + octavesOffset[i].x;
                float calculatedY = (float) (y - halfHeight) / noiseScale * frequency + octavesOffset[i].y;

                float noiseValue = Mathf.PerlinNoise (calculatedX, calculatedY);
                heightValue += noiseValue * amplitudes;

                amplitudes *= persistance;
                frequency *= lacunarity;
                
            }

            // Set noise min, max value
            // Min = max, max = min to always set value for min, max variable when looping
            if (maxValue < heightValue) {
                maxValue = heightValue;
            }
            
            if (minValue > heightValue) {
                minValue = heightValue;
            }

            noiseMap[x,y] = heightValue;
        }
    }

    for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {
            noiseMap[x,y] = Mathf.InverseLerp (minValue, maxValue, noiseMap[x,y]);
        }
    }

    return noiseMap;
   } 
}
