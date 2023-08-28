using System.Collections.Generic;
using UnityEngine;

public class BoardUtilities
{
    public static Vector3 GetEnemyNewPosition(Vector3 currentEnemyPosition, HashSet<Vector3> currentEnemiesPositions,Vector3[,] tilePositions)
    {
        currentEnemiesPositions.Remove(currentEnemyPosition);
        Vector3 newPosition = GeneratePosition(tilePositions);
        
        while (currentEnemiesPositions.Contains(newPosition))
        {
            newPosition = GeneratePosition(tilePositions);
        }

        currentEnemiesPositions.Add(newPosition);
        
        return newPosition;
    }
    
    private static Vector3 GeneratePosition(Vector3[,] tilePositions)
    {
        int randomXIndex = Random.Range(0, tilePositions.GetLength(0));
        int randomZIndex = Random.Range(0, tilePositions.GetLength(1));
        Vector3 position = tilePositions[randomXIndex, randomZIndex];
        position.y += 1;

        return position;
    }
}
