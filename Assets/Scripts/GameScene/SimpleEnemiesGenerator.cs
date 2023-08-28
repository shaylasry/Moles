using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemiesGenerator : MonoBehaviour
{
     [SerializeField] private int _numOfEnemies;
     [SerializeField] private Enemy _enemy;
     
     public Enemy [] GenerateEnemies(Vector3 [,] boardTilePositions, HashSet<Vector3> currentEnemiesPositions)
     {
          Enemy[] enemies = new Enemy[_numOfEnemies];

          for (int i = 0; i < _numOfEnemies; i++)
          {
               Vector3 enemyPosition = BoardUtilities.GetEnemyNewPosition(transform.position ,currentEnemiesPositions, boardTilePositions);
               currentEnemiesPositions.Add(enemyPosition);
               
               enemies[i] = Instantiate(_enemy, enemyPosition, Quaternion.identity, transform);
          }

          return enemies;
     }
}