using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    [SerializeField] private GameObject _prefab;
    
    public GameObject GeneratePlayer()
    {
        Vector2 boardSize = new Vector2(_boardData.width * _boardData.tile.width,
            _boardData.height * _boardData.tile.height);

        float startX = -boardSize.x / 2 + _boardData.tile.width / 2;
        float startZ = boardSize.y / 2 - _boardData.tile.height / 2;

        Vector3 startPos = new Vector3(startX, 1f, startZ);

        Debug.Log(startPos);
        GameObject player = Instantiate(_prefab, startPos, Quaternion.identity);

        return player;
    }
}
