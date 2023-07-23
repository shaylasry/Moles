using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private Board board;
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    
    
    private Vector2 _movementInput;
    private Vector3 _playerMovement;
    private float _playerSpeed = 2.0f;

    
    private bool _isMovementPressed;
    [SerializeField] private float rotationFactorPerFrame = 1.0f;
    
    
    private void Awake()
    {
        InitPlayerInput();
        _characterController = GetComponent<CharacterController>();
    }
    

 

    private void Update()
    {
        HandleMovementWithPlayerInput();
        // HandleMovement();
        // _characterController.Move(_playerMovement * Time.deltaTime * _playerSpeed);
    }

    private void OnEnable()
    {
        _playerInput.PlayerContorls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.PlayerContorls.Disable();
    }

    public Board GetGameBoard()
    {
        return AssetDatabase.LoadAssetAtPath<Board>("Assets/Data/Board.asset");
    }

    
    private void HandleMovement()
    {
        
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        Vector3 newPosition = transform.position + moveDirection * Time.deltaTime * _playerSpeed;
    
      
        RaycastHit hit;
        float raycastDistance = 1f; 
        if (Physics.Raycast(newPosition, Vector3.down, out hit, raycastDistance))
        {
            _characterController.Move(moveDirection * Time.deltaTime * _playerSpeed);
        }
        
    }

    private void HandleRotation()
    {
        throw new System.NotImplementedException();
    }
    
    private void HandleCollision()
    {
        throw new System.NotImplementedException();
    }

    private void HandlePhysics()
    {
        throw new System.NotImplementedException();
    }
    
    private void HandleMovementWithPlayerInput()
    {
        // Vector3 moveDirection = new Vector3(_playerMovement);
        _playerMovement = transform.TransformDirection(_playerMovement);
        Vector3 newPosition = transform.position + _playerMovement * Time.deltaTime * _playerSpeed;
    
      
        RaycastHit hit;
        float raycastDistance = 1f; 
        if (Physics.Raycast(newPosition, Vector3.down, out hit, raycastDistance))
        {
            _characterController.Move(_playerMovement * Time.deltaTime * _playerSpeed);
        }
    }
    private void InitPlayerInput()
    {
        _playerInput = new PlayerInput();
        
        _playerInput.PlayerContorls.Movement.started += OnMovementInput;
        _playerInput.PlayerContorls.Movement.performed += OnMovementInput;
        _playerInput.PlayerContorls.Movement.canceled += OnMovementInput;
    }
    
    private void OnMovementInput(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();

        _playerMovement.x = _movementInput.x;
        _playerMovement.y = 0.0f;
        _playerMovement.z = _movementInput.y;
        _isMovementPressed = _movementInput.x != 0 || _movementInput.y != 0;
        
    }
    

    private void HandleRotationWithPlayerInput()
    {
        Vector3 playerDirection = new Vector3(_movementInput.x, 0.0f, _movementInput.y);
        

        Quaternion currentRotation = transform.rotation;
        if (_isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
            Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }


   
}