using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Board", fileName = "Board")]
public class Board : ScriptableObject
{
    public float width;
    public float height;
    public FloorTile tile;
    
}
