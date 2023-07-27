using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Board Data", fileName = "BoardData")]
public class BoardData : ScriptableObject
{
    public float width;
    public float height;
    public FloorTile tile;
}
