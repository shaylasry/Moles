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
    
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _boardGenerator = GetComponent<BoardGenerator>();
        _playerGenerator = GetComponent<PlayerGenerator>();
        // _playerController = GetComponent<PlayerController>();
        _playerInput = GetComponent<PlayerInput>();
        
        _player = _playerGenerator.GeneratePlayer();
        _player.GetComponentInParent<PlayerController>();
        // _playerInput.

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
