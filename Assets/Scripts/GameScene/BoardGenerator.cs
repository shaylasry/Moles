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
        float initialX = (-_boardData.width / 2) + (_boardData.tile.width / 2);
        float initialZ = (-_boardData.height / 2) + (_boardData.tile.height / 2);
        
        for (int i = 0; i < _boardData.width; i++)
        {
            for (int j = 0; j < _boardData.height; j++)
            {
                Vector3 position = new Vector3(initialX + j * _boardData.tile.width, 0, initialZ + i * _boardData.tile.height);
                _tiles[i,j] = Instantiate(_boardData.tile.tilePrefab, position, Quaternion.identity, transform);
            }
        }

        GenerateGrass();
    }

    private void GenerateGrass()
    {
        foreach (GameObject tile in _tiles)
        {
            for (int i = 0; i < _boardData.tile.grassPerTile; i++)
            {
                HashSet<Vector3> grassPoistions = new HashSet<Vector3>();
                Vector3 spawnPosition = GetGrassPosition(tile.transform.position);
                while (grassPoistions.Contains(spawnPosition))
                {
                    spawnPosition = GetGrassPosition(tile.transform.position);
                }

                grassPoistions.Add(spawnPosition);
                Instantiate(_boardData.tile.grassPrefab, spawnPosition, Quaternion.identity, tile.transform);
            }
        }
    }

    private Vector3 GetGrassPosition(Vector3 position)
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-_boardData.tile.width / 2, _boardData.tile.width / 2),
            0,
            Random.Range(-_boardData.tile.height / 2, _boardData.tile.height / 2)
        );
        
        return position + randomOffset;
    }
}