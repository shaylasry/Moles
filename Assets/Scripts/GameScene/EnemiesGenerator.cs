using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private int numOfMoles;
    private List<GameObject> _moles = new List<GameObject>();
    private bool boardDidLoad = false;
    private Vector3[,] boardData;
    
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    
    void Start()
    {
        StartCoroutine(MolesMovementCoroutine());
    }

    private void GenerateMoles()
    {
        for (int i = 0; i < numOfMoles; i++)
        {
            int randomXIndex = Random.Range(0, boardData.GetLength(0));
            int randomZIndex = Random.Range(0, boardData.GetLength(1));
            Vector3 position = boardData[randomXIndex, randomZIndex];
            _moles.Add(Instantiate(molePrefab, position, Quaternion.identity, transform));
        }
    }
    
    IEnumerator MolesMovementCoroutine()
    {
        while (boardDidLoad)
        {
            foreach (GameObject mole in _moles)
            {
                int randomXIndex = Random.Range(0, boardData.GetLength(0));
                int randomZIndex = Random.Range(0, boardData.GetLength(1));
                Vector3 newPosition = boardData[randomXIndex, randomZIndex];
                mole.transform.position = newPosition;
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private void SubscribeEvents()
    {
        BoardGenerator.BoardDidLoad += OnBoardDidLoad;
    }

    private void UnsubscribeEvents()
    {
        BoardGenerator.BoardDidLoad -= OnBoardDidLoad;
    }

    private void OnBoardDidLoad(BoardGenerator boardGenerator)
    {
        boardData = boardGenerator.boardData;
        GenerateMoles();
        boardDidLoad = true;
    }
}
