// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// public class EnemiesGenerator : MonoBehaviour
// {
//     [SerializeField] private GameObject molePrefab;
//     [SerializeField] private int numOfMoles;
//     private List<GameObject> _moles = new List<GameObject>();
//     private bool boardDidLoad = false;
//     private Vector3[,] boardData;
//     
//     private void OnEnable()
//     {
//         SubscribeEvents();
//     }
//
//     private void OnDisable()
//     {
//         UnsubscribeEvents();
//     }
//     
//     void Start()
//     {
//         StartCoroutine(MolesMovementCoroutine());
//     }
//
//     private void GenerateMoles()
//     {
//         for (int i = 0; i < numOfMoles; i++)
//         {
//             _moles.Add(Instantiate(molePrefab, GetNewMolePosition(), Quaternion.identity, transform));
//         }
//     }
//     
//     IEnumerator MolesMovementCoroutine()
//     {
//         while (boardDidLoad)
//         {
//             foreach (GameObject mole in _moles)
//             {
//                 mole.transform.position = GetNewMolePosition();
//             }
//
//             yield return new WaitForSeconds(3f);
//         }
//     }
//     
//     private Vector3 GetNewMolePosition()
//     {
//         Vector3 newPosition = GeneratePosition();
//         //TO DO: will check if there is another object that we might collide with.
//         // RaycastHit hit;
//         // float raycastDistance = 0.00f; 
//         // while (Physics.Raycast(newPosition, Vector3.down, out hit, raycastDistance))
//         // {
//         //     newPosition = GeneratePosition();
//         // }
//         return newPosition;
//     }
//     private Vector3 GeneratePosition()
//     {
//         int randomXIndex = Random.Range(0, boardData.GetLength(0));
//         int randomZIndex = Random.Range(0, boardData.GetLength(1));
//         Vector3 position = boardData[randomXIndex, randomZIndex];
//         position.y += 1;
//
//         return position;
//     }
//     
//     private void SubscribeEvents()
//     {
//         BoardGenerator.BoardDidLoad += OnBoardDidLoad;
//     }
//
//     private void UnsubscribeEvents()
//     {
//         BoardGenerator.BoardDidLoad -= OnBoardDidLoad;
//     }
//
//     private void OnBoardDidLoad(BoardGenerator boardGenerator)
//     {
//         boardData = boardGenerator.boardData;
//         GenerateMoles();
//         boardDidLoad = true;
//     }
// }


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
    private HashSet<Vector3> _staticEnemies = new HashSet<Vector3>();
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
        HashSet<Vector3> currentMolesPositions = new HashSet<Vector3>();
        for (int i = 0; i < numOfMoles; i++)
        {
            _moles.Add(Instantiate(molePrefab, GetNewMolePosition(currentMolesPositions), Quaternion.identity, transform));
        }
    }
    
    IEnumerator MolesMovementCoroutine()
    {
        while (boardDidLoad)
        {
            HashSet<Vector3> currentMolesPositions = new HashSet<Vector3>();
            foreach (GameObject mole in _moles)
            {
                Vector3 newMolePosition = GetNewMolePosition(currentMolesPositions);
                mole.transform.position = newMolePosition;
                currentMolesPositions.Add(newMolePosition);
            }

            yield return new WaitForSeconds(3f);
        }
    }
    
    private Vector3 GetNewMolePosition(HashSet<Vector3> currentMolesPositions)
    {
        Vector3 newPosition = GeneratePosition();
        
        while (!IsPositionValid(newPosition, currentMolesPositions))
        {
            newPosition = GeneratePosition();
        }
        
        return newPosition;
    }
    
    private bool IsPositionValid(Vector3 position, HashSet<Vector3> currentMolesPositions)
    {
        return !currentMolesPositions.Contains(position) && !_staticEnemies.Contains(position);
    }
    private Vector3 GeneratePosition()
    {
        int randomXIndex = Random.Range(0, boardData.GetLength(0));
        int randomZIndex = Random.Range(0, boardData.GetLength(1));
        Vector3 position = boardData[randomXIndex, randomZIndex];
        position.y += 1;

        return position;
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
