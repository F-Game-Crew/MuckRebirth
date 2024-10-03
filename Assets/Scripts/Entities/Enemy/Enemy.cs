using UnityEngine;


public abstract class Enemy : Entity
{
    public abstract GameObject GetEnemyPrefab();
    public abstract void SetEnemyPrefab(GameObject gameObject);
}
