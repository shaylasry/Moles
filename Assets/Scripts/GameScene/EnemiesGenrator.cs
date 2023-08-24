using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _molePrefab;
    [SerializeField] private int _numOfMoles;
    private List<GameObject> _moles = new List<GameObject>();
    private HashSet<Vector3> _staticEnemies = new HashSet<Vector3>();
    private bool _isGameRunning;
    private Vector3[,] _boardTilePositions;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();   
    }

    private void Subscribe()
    {
        GameStateMachine.onGameStateChange += GetGameState;
    }

    private void Unsubscribe()
    {
        GameStateMachine.onGameStateChange -= GetGameState;
    }
    
    private void GetGameState(bool isGameRunning)
    {
        _isGameRunning = isGameRunning;

        if (_isGameRunning)
        {
            StartCoroutine(MolesMovementCoroutine());
        }
        else
        {
            StopCoroutine(MolesMovementCoroutine());
        }
    }

    public void GenerateMoles(Vector3[,] tilePositions)
    {
        _boardTilePositions = tilePositions;
        HashSet<Vector3> currentMolesPositions = new HashSet<Vector3>();
        for (int i = 0; i < _numOfMoles; i++)
        {
            _moles.Add(Instantiate(_molePrefab, GetNewMolePosition(currentMolesPositions, tilePositions), Quaternion.identity, transform));
        }
    }

    IEnumerator MolesMovementCoroutine()
    {
        while (_isGameRunning)
        {
            HashSet<Vector3> currentMolesPositions = new HashSet<Vector3>();
            foreach (GameObject mole in _moles)
            {
                Vector3 newMolePosition = GetNewMolePosition(currentMolesPositions, _boardTilePositions);
                mole.transform.position = newMolePosition;
                currentMolesPositions.Add(newMolePosition);
            }

            yield return new WaitForSeconds(3f);
        }
    }
    
    private Vector3 GetNewMolePosition(HashSet<Vector3> currentMolesPositions, Vector3[,] tilePositions)
    {
        Vector3 newPosition = GeneratePosition(tilePositions);
        
        while (!IsPositionValid(newPosition, currentMolesPositions))
        {
            newPosition = GeneratePosition(tilePositions);
        }
        
        return newPosition;
    }
    
    private bool IsPositionValid(Vector3 position, HashSet<Vector3> currentMolesPositions)
    {
        return !currentMolesPositions.Contains(position) && !_staticEnemies.Contains(position);
    }
    
    private Vector3 GeneratePosition(Vector3[,] tilePositions)
    {
        int randomXIndex = Random.Range(0, tilePositions.GetLength(0));
        int randomZIndex = Random.Range(0, tilePositions.GetLength(1));
        Vector3 position = tilePositions[randomXIndex, randomZIndex];
        position.y += 1;

        return position;
    }
}