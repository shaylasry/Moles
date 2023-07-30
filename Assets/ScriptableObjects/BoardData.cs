using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/BoardData", fileName = "BoardData")]
public class BoardData : ScriptableObject
{
    public int width;
    public int height;
    public FloorTile tile;
}