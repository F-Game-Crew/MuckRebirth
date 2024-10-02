using UnityEngine;

public class TreeEntity
{
    private GameObject treePrefab;
    private Transform parentTransform;
    private Vector3 spawnPosition;
    
    public TreeEntity (GameObject treePrefab, Vector3 spawnPosition, Transform parentTransform) {
        this.treePrefab = treePrefab;
        this.parentTransform = parentTransform;
        this.spawnPosition = spawnPosition;
    }

    public void Spawn (int sizeScale) {
        GameObject tree = Object.Instantiate(this.treePrefab, this.spawnPosition, Quaternion.identity, this.parentTransform);
        tree.transform.localScale = new Vector3(sizeScale, sizeScale, sizeScale);
        tree.AddComponent<MeshCollider>();
    }
}
