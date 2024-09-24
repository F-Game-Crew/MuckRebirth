using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    public PlayerOne createPlayerOne(string s)
    {
        if (s == "p")
        {
            var player = new PlayerOne();
            player.SetPlayerPrefabs(playerPrefab);
            return player;
        }
        else
        {
            return null;
        }
    }
}
