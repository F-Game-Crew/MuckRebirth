using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  public enum DrawMode {
    Noise,
    Color,
    Mesh,
    Falloff
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
  MapDisplay display;
  

  public void Start () {
    Generate();
  }

  public void Generate () {
    (float[,], float[,]) noise = NoiseGenerator.NewTerrain (
      mapChunkSize,
      seed,
      noiseScale,
      octaves,
      lacunarity,
      persistance,
      offset,
      useFalloff
      ); 

    display = FindObjectOfType<MapDisplay>();

    if (drawMode == DrawMode.Noise) {
        display.DrawTexture(
          MapTextureGenerator.NoiseFromHeightMap(noise.Item1),
          mapChunkSize,
          mapChunkSize
        );
    } else if (drawMode == DrawMode.Color) {
        display.DrawTexture (
          MapTextureGenerator.ColorFromHeightMap(noise.Item1, regions),
          mapChunkSize,
          mapChunkSize
        );
    } else if (drawMode == DrawMode.Mesh) {
        display.DrawMesh(
          MeshGenerator.NewTerrain(noise.Item1, heightCurve, heightMultipler),
          MapTextureGenerator.ColorFromHeightMap(noise.Item1, regions),
          meshScale
        );
      } else if (drawMode == DrawMode.Falloff) {
        display.DrawTexture(
          MapTextureGenerator.NoiseFromHeightMap(noise.Item2),
          mapChunkSize,
          mapChunkSize
        );;
    }
  }

  void OnValidate () {
    if (noiseScale < 0) {
      noiseScale = 0.0001f;
    }
  }
}
