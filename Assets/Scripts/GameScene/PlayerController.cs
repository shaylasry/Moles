using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    private Bounds _boardBounds;
    
    [SerializeField] private float _movementSpeed = 3f;
    private Vector2 _currentDirection = Vector2.right;  // Initial direction

    private void Start()
    {
        SetInitialPosition();
        _boardBounds = CalculateBounds();
    }

    void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
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
    }

    private void SetInitialPosition()
    {
        float startX = -1 * (int)_boardData.width / 2 * _boardData.tile.width + _boardData.tile.width / 2;
        float startY = 1f;
        float startZ = (int)_boardData.height / 2 * _boardData.tile.height - _boardData.tile.width / 2;

        Vector3 startPos = new Vector3(startX, startY, startZ);

        transform.position = startPos;
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
        Vector3 center = new Vector3(0, 1, 0);
    
        return new Bounds(center, boardSize);
    }
}