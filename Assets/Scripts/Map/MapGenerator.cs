using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode {
        Noise,
        Color,
        Mesh
    }

    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetails;
    public int seed;
    public int octaves_num;

    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public float noiseScale;
    public float meshHeightMultipler;

    public bool autoUpdate;

    public DrawMode drawMode;
    public Vector2 offset;
    public TerrainType[] regions;
    public AnimationCurve animationCurve;

    public void Start () {
        Generate();
    }

    public void Generate() {
        float[,] noiseMap = NoiseMapGenerator.New(mapChunkSize, mapChunkSize, noiseScale, seed, octaves_num, persistance, lacunarity, offset);
        
        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.Noise) {
            display.DrawNoiseMap(noiseMap);
        } else if (drawMode == DrawMode.Color) {
            display.DrawColorMap(noiseMap, regions);
        } else {
           display.DrawMeshMap(MeshGenerator.GenerateTerrain(noiseMap, meshHeightMultipler, animationCurve, levelOfDetails), noiseMap, regions);
        };
    }

    void OnValidate () {
        if (octaves_num == 0) {
            octaves_num = 0;
        }
    }
}