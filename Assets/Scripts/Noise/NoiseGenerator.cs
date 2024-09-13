using System.Linq;
using UnityEngine;

public class NoiseGenerator
{
   public static (float[,], float[,]) NewTerrain (int size, int seed, float noiseScale, int octaves, float lacunarity, float persistance, Vector2 offset, bool useFalloff) {
    float[,] noiseMap = new float[size, size];
    float[,] falloffMap = FalloffGenerator.New(size);

    System.Random rand = new System.Random(seed);
    Vector2[] octavesOffset = new Vector2[octaves]; 
    for (int i = 0; i < octaves; i++) {
        float xOffset = rand.Next(-100000, 100000) + offset.x;
        float yOffset = rand.Next(-100000, 100000) + offset.y;
        octavesOffset[i] = new Vector2(xOffset, yOffset);
    }

    float minValue = float.MaxValue;
    float maxValue = float.MinValue;

    float halfSize= size / 2;

    for (int y = 0; y < size; y++) {
        for (int x = 0; x < size; x++) {
            float amplitudes = 1;
            float frequency = 1;
            float heightValue = 1;

            for (int i = 0; i < octaves; i++) {
                float calculatedX = (float) (x - halfSize) / noiseScale * frequency + octavesOffset[i].x;
                float calculatedY = (float) (y - halfSize) / noiseScale * frequency + octavesOffset[i].y;

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

    for (int y = 0; y < size; y++) {
        for (int x = 0; x < size; x++) {
            if (useFalloff) {
                noiseMap[x,y] = Mathf.Clamp01(Mathf.InverseLerp (minValue, maxValue, noiseMap[x,y]) - falloffMap[x,y]);
            } else {
                noiseMap[x,y] = Mathf.InverseLerp (minValue, maxValue, noiseMap[x,y]);
            }
        }
    }

    return (noiseMap, falloffMap);
   } 
}
