using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    public GameObject prefab;

    public Monster CreateMonster(MonsterType type)
    {
        Monster monster;
        switch (type)
        {
            case MonsterType.Skeleton:
                monster = new Monster();
                monster.SetPrefab(prefab);
                return monster;
            default:
                throw new System.Exception("Your monster type is not exist in our monster system!");
        }
    }
}
