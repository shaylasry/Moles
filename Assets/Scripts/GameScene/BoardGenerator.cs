using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private BoardData boardData;
    
    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        float initialX = (-boardData.width / 2) + (boardData.tile.width / 2);
        float initialZ = (-boardData.height / 2) + (boardData.tile.height / 2);

        for (int i = 0; i < boardData.width; i++)
        {
            for (int j = 0; j < boardData.height; j++)
            {
                Vector3 position = new Vector3(initialX + j * boardData.tile.width, 0, initialZ + i * boardData.tile.height);
                Instantiate(boardData.tile.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}