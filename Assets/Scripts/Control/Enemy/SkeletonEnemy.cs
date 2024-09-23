using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    [SerializeField] private GameObject _enemyPrefabs;

    public override GameObject GetEnemyPrefab()
    {
        Debug.Log("Skeleton Created");
        return _enemyPrefabs;
    }

    public override void SetEnemyPrefab(GameObject gameObject)
    {
        _enemyPrefabs = gameObject;
    }
}
