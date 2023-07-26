using System;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public static event Action<BoardGenerator> BoardDidLoad;
    [SerializeField] private Board board; 
    public Vector3[,] boardData { get; private set; }

    private void Awake()
    {
        GenerateBoardData();
        InstantiateBoard(boardData);
        BoardDidLoad?.Invoke(this);
    }
    
    private void GenerateBoardData()
    {
        int width = board.width;
        int height = board.height;
        FloorTile tile = board.tile;
        boardData = new Vector3[width, height];
        
        Vector3 position = transform.position;
        float initialPositionX = position.x - (board.width / 2) + (board.tile.width / 2);
        float initialPositionZ = position.z - (board.height / 2) + (board.tile.height / 2);
        float initialPositionY = position.y;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 cellPosition = new Vector3(initialPositionX + j * tile.width, initialPositionY, initialPositionZ + i * tile.height);
                boardData[i, j] = cellPosition;
            }
        }
    }
    
    private void InstantiateBoard(Vector3[,] gridData)
    {
        for (int i = 0; i < gridData.GetLength(0); i++)
        {
            for (int j = 0; j < gridData.GetLength(1); j++)
            {
                // Instantiate the cell prefab at the stored position
                Instantiate(board.tile.prefab, gridData[i, j], Quaternion.identity, transform);
            }
        }
    }
}