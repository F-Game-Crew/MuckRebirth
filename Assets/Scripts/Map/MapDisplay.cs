using UnityEngine;

public class MapDisplay : MonoBehaviour
{
   public Renderer textureRenderer; 
   public MeshFilter meshFilter;
   public MeshRenderer meshRenderer;

    public void DrawTexture (Texture2D texture, int width, int height) {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }

    public void DrawMesh (Mesh mesh, Texture2D texture, float meshScale) {
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture;
        meshRenderer.transform.localScale = new Vector3(meshScale, meshScale, meshScale);
    }
}
