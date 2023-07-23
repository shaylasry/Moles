using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{

    [SerializeField] private Board _board;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        float initialX = (-_board.width / 2) + (_board.tile.width / 2);
        float initialZ = (-_board.height / 2) + (_board.tile.height / 2);

        for (int i = 0; i < _board.width; i++)
        {
            for (int j = 0; j < _board.height; j++)
            {
                Vector3 position = new Vector3(initialX + j * _board.tile.width, 0, initialZ + i * _board.tile.height);
                Instantiate(_board.tile.prefab, position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}