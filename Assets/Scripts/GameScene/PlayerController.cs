using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    private Bounds _boardBounds;
    
    [SerializeField] private float _movementSpeed = 5f;
    private Vector2 _currentDirection = Vector2.right;  // Initial direction
    [SerializeField] private float _rotationSpeed = 50.0f;
    
    private float _audioPitch = 1f;
    private float _grassPopCooldown;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _grassPopAudioClip;

    private void Start()
    {
        SetInitialPosition();
        _boardBounds = CalculateBounds();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleMovement();
        HandlePitch();
    }

    private void HandleMovement()
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
        if (_boardBounds.Contains(newPos))
        {
            Vector3 translation = (newPos - transform.position) * _movementSpeed * Time.deltaTime;
            transform.Translate(translation);
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        _currentDirection = context.ReadValue<Vector2>();
        HandleRotation();
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
        Debug.Log("[test] colision");
        if (other.gameObject.CompareTag("GrassBlade"))
        {
            Destroy(other.gameObject);
            HandleGrassBladeCollision();
        }
    }
    
    private void SetInitialPosition()
    {
        float startX = -1 * (int)_boardData.width / 2 * _boardData.tile.width + _boardData.tile.width / 2;
        float startY = 1f;
        float startZ = (int)_boardData.height / 2 * _boardData.tile.height - _boardData.tile.width / 2;

        Vector3 startPos = new Vector3(startX, startY, startZ);

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
    
        Vector3 tileSize = new Vector3(tile.width, 0f, tile.height);
        Vector3 boardSize = new Vector3( _boardData.width * tileSize.x, 1f,  _boardData.height * tileSize.z);
        Vector3 center = _boardData.center;
        center.y += 1;

        return new Bounds(center, boardSize);
    }
    
    private void HandleGrassBladeCollision()
    {
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
        
        _audioSource.pitch = _audioPitch;
    }
}