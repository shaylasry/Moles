using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private bool _isGameRunning;

    private void Awake()
    {
        _gameManager = GetComponentInParent<GameManager>();
        if (_gameManager == null)
        {
            Debug.Log("couldn't find game manager");
        }
    }

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
        GameManager.GameRunningStateDidChange += OnGameRunningStateChange;
    }
    
    private void Unsubscribe()
    {
        GameManager.GameRunningStateDidChange -= OnGameRunningStateChange;
    }

    private void OnGameRunningStateChange(bool isGameRunning)
    {
        _isGameRunning = isGameRunning;
    
        if (_isGameRunning)
        {
            StartCoroutine(EnemyMovementCoroutine());
        }
        else
        {
            StopCoroutine(EnemyMovementCoroutine());
        }
    }
    
    IEnumerator EnemyMovementCoroutine()
    {
        while (_isGameRunning)
        {
            Vector3 newPosition = BoardUtilities.GetEnemyNewPosition(transform.position, _gameManager.currentEnemiesPositions, _gameManager.boardTilePositions);
            transform.position = newPosition;
    
            yield return new WaitForSeconds(3f);
        }
    }
    
}