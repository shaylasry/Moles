using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    public Vector3[,] tilePositions { get; private set; }
    private GameObject[,] _tiles; 
    
    private EnemiesGenerator _enemiesGenerator;

    public void Awake()
    {
        _enemiesGenerator = GetComponent<EnemiesGenerator>();
    }

    public void GenerateBoard()
    {
        _tiles = new GameObject[(int)_boardData.width,(int)_boardData.height];
        tilePositions = new Vector3[(int)_boardData.width, (int)_boardData.height];
        
        float initialX = (-_boardData.width * _boardData.tile.width / 2) + (_boardData.tile.width / 2);
        float initialZ = (-_boardData.height * _boardData.tile.height / 2) + (_boardData.tile.height / 2);
        
        for (int i = 0; i < _boardData.width; i++)
        {
            for (int j = 0; j < _boardData.height; j++)
            {
                Vector3 position = new Vector3(initialX + j * _boardData.tile.width, 0, initialZ + i * _boardData.tile.height);
                
                tilePositions[i, j] = position;
                _tiles[i,j] = Instantiate(_boardData.tile.tilePrefab, position, Quaternion.identity, transform);
            }
        }
        
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        _enemiesGenerator.GenerateMoles(tilePositions);
    }
}