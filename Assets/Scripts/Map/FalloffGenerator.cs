using UnityEngine;

public class FalloffGenerator
{
   public static float[,] New (int mapSize) {
        float[,] map = new float[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++ ) {
                float x = i / (float) mapSize * 2 - 1;
                float y = j / (float) mapSize * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i,j] = Evaluate(value);
            }          
        } 

        return map;
   }

   static float Evaluate(float value) {
    float a = 6;
    float b = 5.5f;

    return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
   }
}
