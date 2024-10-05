using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public MonsterFactory monsterFactory;
    [SerializeField] public PlayerFactory playerFactory;
    [SerializeField] private Transform targetTransform;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            float x = Random.Range(targetTransform.position.x - 10, targetTransform.position.x + 10);
            float z = Random.Range(-targetTransform.position.z - 10, targetTransform.position.z + 10);
            Instantiate(monsterFactory.CreateMonster(MonsterType.Skeleton).GetPrefab(), new Vector3(x, 0, z), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            float x = transform.position.x;
            float z = transform.position.z;
            Instantiate(playerFactory.CreatePlayer(PlayerType.Default).GetPrefab(), transform.position, Quaternion.identity);
        }
    }
}
