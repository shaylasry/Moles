using System;

using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private Player player;
    

    public void Start()
    {
        GeneratePlayer();
    }

    private void GeneratePlayer()
    {
        Instantiate(player.prefab, new Vector3(0, 1, 0), Quaternion.identity);
    }

    
    
}