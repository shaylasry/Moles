using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BoardData boardData;

    private void Start()
    {
        SetInitialPosition();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector2 movement = context.ReadValue<Vector2>();

        Vector3 newPos = transform.position;

        if (movement.x != 0)
        {
            float tileWidth = boardData.tile.width;
            float newX = transform.position.x + (movement.x > 0 ? tileWidth : -tileWidth);
            newPos.x = newX;
        }
        else if (movement.y != 0)
        {
            float tileHeight = boardData.tile.height;
            float newZ = transform.position.z + (movement.y > 0 ? tileHeight : -tileHeight);
            newPos.z = newZ;
        }

        transform.position = newPos;
        
        Debug.Log($"[TEST]: received movement: {movement}");
    }

    private void SetInitialPosition()
    {
        float startX = -1 * (int)boardData.width / 2 * boardData.tile.width + boardData.tile.width / 2;
        float startY = 1f;
        float startZ = (int)boardData.height / 2 * boardData.tile.height - boardData.tile.width / 2;

        Vector3 startPos = new Vector3(startX, startY, startZ);

        transform.position = startPos;
    }
}