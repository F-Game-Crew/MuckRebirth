using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    public Player CreatePlayer(PlayerType type)
    {
        switch (type)
        {
            case PlayerType.Default:
                Player player = new Player();
                player.SetPrefab(prefab);
                return player;
            default:
                throw new System.Exception("Your player type is not exist in our player system!");
        }
    }
}
