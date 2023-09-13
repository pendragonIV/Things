using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileData", menuName = "Map/TileData")]
public class TileData : ScriptableObject
{

    public List<TileBase> tiles;

    public bool isPlowable;

}
