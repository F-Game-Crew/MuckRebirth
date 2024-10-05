using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    private GameObject prefab;

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public void SetPrefab(GameObject pf)
    {
        prefab = pf;
    }
}
