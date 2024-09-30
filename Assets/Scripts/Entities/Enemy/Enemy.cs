using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Enemy
{
    public abstract GameObject GetEnemyPrefab();
    public abstract void SetEnemyPrefab(GameObject gameObject);
}
