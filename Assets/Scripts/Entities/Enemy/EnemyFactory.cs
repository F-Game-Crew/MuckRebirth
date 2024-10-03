using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject skeletonPrefab;

    public Enemy CreateEnemy(string s)
    {
        Enemy enemy = null;
        if (s == "m")
        {
            enemy = new SkeletonEnemy();
            enemy.SetEnemyPrefab(skeletonPrefab);
            return enemy;
        }
        else
        {
            Debug.Log("Fail spawn");
            return null;
        }

    }
}
