using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Animator _animator;
    private SkeletonEnemyLocation _skeletonEnemyLocation;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _skeletonEnemyLocation = GetComponent<SkeletonEnemyLocation>();
    }

    private void Update()
    {
        _skeletonEnemyLocation.HandleAllAction();
    }
}
