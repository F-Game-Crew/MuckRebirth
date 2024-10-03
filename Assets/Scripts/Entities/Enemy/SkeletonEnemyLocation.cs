using UnityEngine;

public class SkeletonEnemyLocation : MonoBehaviour
{
    [Header("Attribute Enemy")]
    public float healthPoint;

    public float rangeAttack;
    public float rangeLook;
    public float speedAttack;
    public float damageAttack;
    public float smoothTime;
    public int dayToSummon;
    public int typeOfEnemies;
    public Vector3 directionSummoned;
    public int amountOfEnemiesSpawnInOneNight;
    [Header("Enemy Movement")]
    public Vector3 moveVelocity = Vector3.zero;
    private Transform _targetTransform;
    public Rigidbody rigidbody;
    public AnimatorManagerEnemy animatorManager;

    // ==========
    // Attributes
    // ==========
    private bool haveEntityInView;
    private Viewable view;

    private void Awake()
    {
        _targetTransform = FindObjectOfType<PlayerManager>().transform;
        animatorManager = FindObjectOfType<AnimatorManagerEnemy>();
        rigidbody = GetComponent<Rigidbody>();
        view = GetComponent<Viewable>();
        haveEntityInView = false;
    }

    private void Update()
    {
        haveEntityInView = view.entitiesInView.Count > 0;
    }

    public void HandleAllAction()
    {
        if (haveEntityInView)
        {
            EnemyMovement();
            EnemyRotation();
        }
    }
    public void EnemyMovement()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, _targetTransform.position,
            ref moveVelocity, smoothTime);
        animatorManager.TargetAnimation("Running", true);
        transform.position = targetPosition;
    }

    public void EnemyAttack()
    {
        throw new System.NotImplementedException();
    }

    public void EnemyRotation()
    {
        Vector3 targetRotation = _targetTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);
    }
}
