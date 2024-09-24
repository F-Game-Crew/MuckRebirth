using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : Player
{
    [SerializeField] private GameObject _playerPrefabs;

    public override GameObject GetPlayerPrefab()
    {
        Debug.Log("Player Created");
        return _playerPrefabs;
    }

    public override void SetPlayerPrefabs(GameObject gb)
    {
        _playerPrefabs = gb;
    }
}
