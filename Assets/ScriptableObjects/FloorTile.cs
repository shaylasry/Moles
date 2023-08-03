using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Floor Tile", fileName = "FloorTile")]
public class FloorTile : ScriptableObject
{
    public float width;
    public float height;
    public int grassPerTile;
    public GameObject tilePrefab;
    public GameObject grassPrefab;
}
