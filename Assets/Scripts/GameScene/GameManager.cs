using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private BoardGenerator _boardGenerator;
    private PlayerGenerator _playerGenerator;
    private PlayerController _playerController;
    
    private PlayerInput _playerInput;
    private GameObject _player;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _boardGenerator = GetComponent<BoardGenerator>();
        _playerGenerator = GetComponent<PlayerGenerator>();
        _playerInput = GetComponent<PlayerInput>();

        GenerateGame();
    }

    private void GenerateGame()
    {
        _boardGenerator.GenerateBoard();
        _player = _playerGenerator.GeneratePlayer();
        // _numOfGrassBlade = GameObject.FindGameObjectsWithTag("GrassBlade").Length;
        
        InputAction moveAction = _playerInput.actions.FindAction("Move");
        moveAction.performed += context => _player.GetComponent<PlayerController>().Move(context);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
