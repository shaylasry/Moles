using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardGenerator _boardGenerator;
    [SerializeField] private PlayerGenerator _playerGenerator;
    [SerializeField] private PlayerInput _playerInput;
    private GameObject _player;

    private void Awake()
    {
        GenerateGame();
    }

    private void GenerateGame()
    {
        _boardGenerator.GenerateBoard();
        _player = _playerGenerator.GeneratePlayer();
        // _numOfGrassBlade = GameObject.FindGameObjectsWithTag("GrassBlade").Length;
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
