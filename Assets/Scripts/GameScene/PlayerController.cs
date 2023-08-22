using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private bool _isGameRunning = false;
    
    [SerializeField] private BoardData _boardData;
    private Bounds _boardBounds;

    [SerializeField] private float _movementDuration = 0.1f;
    private Vector2 _currentDirection = Vector2.right;  // Initial direction
    [SerializeField] private float _rotationSpeed = 50.0f;
    
    private float _audioPitch = 1f;
    private float _grassPopCooldown;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _grassPopAudioClip;
    private int _numOfGrassBlade;
    
    [SerializeField] private ParticleSystem _smokeParticleSystem;

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
        GeneralInputManager.onMove += DidMove;
    }

    private void Unsubscribe()
    {
        GeneralInputManager.onMove -= DidMove;
    }

    private void DidMove(InputAction.CallbackContext context)
    {
        Move(context);
    }

    private void Start()
    {
        SetInitialPosition();
        _boardBounds = CalculateBounds();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        HandlePitch();
    }
    
    IEnumerator MovementCoroutine()
    {
        while (_isGameRunning) //TODO - change the true to boolean that indicates if the game is still running or not
        {
            yield return LerpPosition();
        }
    }

    IEnumerator LerpPosition()
    {
        Vector3 newPos = CalculateNewPosition();

        if (_boardBounds.Contains(newPos))
        {
            float time = 0;
            while (time < _movementDuration)
            {
                transform.position = Vector3.Lerp(transform.position, newPos, time / _movementDuration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = newPos;
        }
    }
 
    private Vector3 CalculateNewPosition()
    {
        Vector3 newPos = transform.position;

        if (_currentDirection.x != 0)
        {
            float tileWidth = _boardData.tile.width;
            float newX = transform.position.x + (_currentDirection.x > 0 ? tileWidth : -tileWidth);
            newPos.x = newX;
        }
        else if (_currentDirection.y != 0)
        {
            float tileHeight = _boardData.tile.height;
            float newZ = transform.position.z + (_currentDirection.y > 0 ? tileHeight : -tileHeight);
            newPos.z = newZ;
        }
        
        return newPos;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (!_isGameRunning)
        {
            _isGameRunning = true;
            StartCoroutine(MovementCoroutine());
            return;
        }
        
        _currentDirection = context.ReadValue<Vector2>();
        HandleRotation();
        HandleSmoke();
    }

    private void HandleRotation()
    {
        Transform childTransform = transform.Find("MowerPlayer");
        Vector3 newDirection = new Vector3(_currentDirection.x, 0f, _currentDirection.y);
        
        if (newDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(newDirection, Vector3.up);
            childTransform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("GrassBlade"))
        {
            HandleGrassBladeCollision(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("DynamicEnemy"))
        {
            Destroy(gameObject);
            Debug.Log("Game Over!");
        }
    }
    
    private void SetInitialPosition()
    {
        Vector2 boardSize = new Vector2(_boardData.width * _boardData.tile.width,
            _boardData.height * _boardData.tile.height);

        float startX = -boardSize.x / 2 + _boardData.tile.width / 2;
        float startZ = boardSize.y / 2 - _boardData.tile.height / 2;

        Vector3 startPos = new Vector3(startX, 1f, startZ);

        transform.position = startPos;
        
        HandleRotation();
    }
    
    public Bounds CalculateBounds()
    {
        FloorTile tile = _boardData.tile;
        
        if (tile == null || tile.tilePrefab == null)
        {
            Debug.LogWarning("FloorTile or its prefab is missing.");
            return new Bounds(Vector3.zero, Vector3.zero);
        }
        
        Vector3 boardSize = new Vector3( _boardData.width * tile.width, 1f,  _boardData.height * tile.height);
        Vector3 center = _boardData.center;
        center.y += 1;

        return new Bounds(center, boardSize);
    }
    
    private void HandleGrassBladeCollision(GameObject gameObject)
    {
        Destroy(gameObject);
        
        _audioPitch += .01f;
        if (_grassPopCooldown <= 0)
        {
            _grassPopCooldown = .05f;
            _audioSource.PlayOneShot(_grassPopAudioClip);
        }
    }
    
    private void HandlePitch()
    {
        if (_grassPopCooldown > 0)
        {
            _grassPopCooldown -= Time.deltaTime;
            _grassPopCooldown = Mathf.Max(0, _grassPopCooldown);
        }

        if (_audioPitch >= 1f)
        {
            _audioPitch -= 1f * Time.deltaTime;
            _audioPitch = Mathf.Max(1f, _audioPitch);
        }
        else if (_audioPitch < 1f)
        {
            // Return the pitch to 1 more quickly
            _audioPitch += 1.5f * Time.deltaTime; 
            _audioPitch = Mathf.Min(1f, _audioPitch);
        }
        
        _audioSource.pitch = _audioPitch;
    }
    
    private void HandleSmoke()
    { 
        _smokeParticleSystem.Play();
    }
}