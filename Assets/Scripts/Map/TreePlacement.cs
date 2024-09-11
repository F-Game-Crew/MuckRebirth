using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacement : MonoBehaviour
{
    public GameObject treePrefab; 
    public int scale;
    public float minHeight;
    public float maxHeight;
    public int treeCount;
    public bool autoUpdate;

    public MeshFilter meshFilter;
    private MapGenerator mapGen;
    private float meshScale;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3 minMaxHeight;

    public void Start () {
        Generate();
    }
    public void Generate() {
        mapGen = transform.parent.GetComponent<MapGenerator>();
        meshScale = mapGen.meshScale;
        mesh = meshFilter.sharedMesh;
        vertices = mesh.vertices;

        Clear();
        Spawn(); 
    }

    public void Spawn () {
        Debug.Log("Tree Generation!");
        CalculateHeightRange();
        int placedTree = 0;

        while (placedTree <= treeCount){
            int randomIndex = Random.Range(0, vertices.Length - 1);
            Vector3 randomPosition = vertices[randomIndex];

            float normalizedHeight = HeightNormalization(randomPosition.y * meshScale);

            if(normalizedHeight > minHeight && normalizedHeight < maxHeight){
                placedTree++;
                randomPosition *= meshScale;
                randomPosition = transform.TransformPoint(randomPosition);
                GameObject tree = Instantiate(treePrefab, randomPosition, Quaternion.identity, transform);
                tree.transform.localScale = new Vector3(scale, scale, scale);
                tree.AddComponent<MeshCollider>();
            }
        }
    }

    public void Clear () {
        for (int i = transform.childCount - 1;i >= 0 ; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


    public void CalculateHeightRange () {
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        foreach (Vector3 vertex in vertices)
        {
            float height = vertex.y * meshScale;

            if (height < minValue) minValue = height;
            if (height > maxValue) maxValue = height;
        }

        minMaxHeight = new Vector3 (minValue, maxValue, 0);
    }

    public float HeightNormalization (float value) {
       float normalizedHeight = Mathf.InverseLerp(minMaxHeight.x, minMaxHeight.y, value);
       return normalizedHeight;
    }

}
