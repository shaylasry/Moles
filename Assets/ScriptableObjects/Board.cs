using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Board", fileName = "Board")]
public class Board : ScriptableObject
{
    public int width;
    public int height;
    public FloorTile tile;
}