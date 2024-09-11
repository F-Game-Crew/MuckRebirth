using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public enum DrawMode {
    Noise,
    Color,
    Mesh
  }
  public DrawMode drawMode;

  const int mapChunkSize = 241;
  
  public bool useFalloff;

  public float noiseScale;
  public int octaves;
  public float lacunarity;
  [Range(0,1)]
  public float persistance;
  public int seed;
  public Vector2 offset;

  public AnimationCurve heightCurve;
  public float heightMultipler;
  public float meshScale;

  public bool autoUpdate;
  public TerrainType[] regions;
  public GameObject treeSpawner;
  MapDisplay display;
  

  public void Start () {
    Generate();
  }

  public void Generate () {
    float[,] noiseMap = NoiseGenerator.NewTerrain (
      mapChunkSize,
      mapChunkSize,
      seed,
      noiseScale,
      octaves,
      lacunarity,
      persistance,
      offset
      ); 

    display = FindObjectOfType<MapDisplay>();

    if (drawMode == DrawMode.Noise) {
      display.DrawTexture(
        MapTextureGenerator.NoiseFromHeightMap(noiseMap),
        mapChunkSize,
        mapChunkSize
        );
    } else if (drawMode == DrawMode.Color) {
      display.DrawTexture (
        MapTextureGenerator.ColorFromHeightMap(noiseMap, regions),
        mapChunkSize,
        mapChunkSize
     );
    } else if (drawMode == DrawMode.Mesh) {
      display.DrawMesh(
        MeshGenerator.NewTerrain(noiseMap, heightCurve, heightMultipler),
        MapTextureGenerator.ColorFromHeightMap(noiseMap, regions),
        meshScale
      );
    }
  }

  void OnValidate () {
    if (noiseScale < 0) {
      noiseScale = 0.0001f;
    }
  }
}
