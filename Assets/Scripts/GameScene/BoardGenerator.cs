using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    private GameObject[,] _tiles; 
    
    void Start()
    {
        GenerateBoard();
    }
    
    private void GenerateBoard()
    {
        _tiles = new GameObject[(int)_boardData.width,(int)_boardData.height];
        
        float initialX = (-_boardData.width * _boardData.tile.width / 2) + (_boardData.tile.width / 2);
        float initialZ = (-_boardData.height * _boardData.tile.height / 2) + (_boardData.tile.height / 2);
        
        for (int i = 0; i < _boardData.width; i++)
        {
            for (int j = 0; j < _boardData.height; j++)
            {
                Vector3 position = new Vector3(initialX + j * _boardData.tile.width, 0, initialZ + i * _boardData.tile.height);
                _tiles[i,j] = Instantiate(_boardData.tile.tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}