using System;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardGenerator : MonoBehaviour
{
    public Vector3[,] tilePositions { get; private set; }
    public GameObject[,] tiles;
    
    private EnemiesGenerator _enemiesGenerator;
    [SerializeField] private BoardData _boardData; 
    

    private void Awake()
    {
        GenerateBoard();
        _enemiesGenerator = GetComponent<EnemiesGenerator>();
        _enemiesGenerator.GenerateMoles(tilePositions);
    }
    
    private void GenerateBoard()
    {
        int width = _boardData.width;
        int height = _boardData.height;
        FloorTile tile = _boardData.tile;
        tilePositions = new Vector3[width, height];
        tiles = new GameObject[width, height];
        
        Vector3 position = transform.position;
        float initialPositionX = position.x - (_boardData.width / 2) + (_boardData.tile.width / 2);
        float initialPositionZ = position.z - (_boardData.height / 2) + (_boardData.tile.height / 2);
        float initialPositionY = position.y;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 cellPosition = new Vector3(initialPositionX + j * tile.width, initialPositionY, initialPositionZ + i * tile.height);
                tilePositions[i, j] = cellPosition;
            }
        }
        
        InstantiateBoard();
    }
    
    private void InstantiateBoard()
    {
        for (int i = 0; i < tilePositions.GetLength(0); i++)
        {
            for (int j = 0; j < tilePositions.GetLength(1); j++)
            {
                // Instantiate the cell prefab at the stored position
                tiles[i, j] = Instantiate(_boardData.tile.prefab, tilePositions[i, j], Quaternion.identity, transform);
            }
        }
    }
}