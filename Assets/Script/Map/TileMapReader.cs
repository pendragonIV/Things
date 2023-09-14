using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReader : MonoBehaviour
{

    [SerializeField]
    private Tilemap tileMap;

    [SerializeField]
    private List<TileData> tileDatas;

    [SerializeField]
    public Dictionary<TileBase, TileData> dataFromTiles;

    public Player player;

    [SerializeField]
    private SpriteRenderer spriteRenderer;




    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();

        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas)
        {
            foreach(var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public TileBase FindNearTile()
    {
        if (player.stateMachine.CurrentState.animationName.Contains("Side"))
        {
            if (spriteRenderer.flipX)
            {
                return FindTileLeft();
            }else
            {
                return FindTileRight();
            }
            
        }
        else if (player.stateMachine.CurrentState.animationName.Contains("Front"))
        {
            return FindTileFront();
        }
        else if (player.stateMachine.CurrentState.animationName.Contains("Back"))
        {
            return FindTileBack();
        }

        return null;
    }

    public Vector3Int GetNearCellPosition(float distance)
    {
        Vector3 objectWorldPosition = player.transform.position;
        objectWorldPosition.y -= .1f;
        if (player.stateMachine.CurrentState.animationName.Contains("Side"))
        {
            if (spriteRenderer.flipX)
            {
                objectWorldPosition.x -= distance;
            }
            else
            {
                objectWorldPosition.x += distance;
            }

        }
        else if (player.stateMachine.CurrentState.animationName.Contains("Front"))
        {
            objectWorldPosition.y -= distance;
            objectWorldPosition.y += .1f;
        }
        else if (player.stateMachine.CurrentState.animationName.Contains("Back"))
        {
            objectWorldPosition.y += distance;
        }

        Vector3Int CellPosition = tileMap.WorldToCell(objectWorldPosition);

        return CellPosition;
    }

    public Vector3 GetNearPosition(Vector3Int cellPosition)
    {
        return tileMap.CellToWorld(cellPosition);
    }


    private TileBase FindTileFront()
    {
        TileBase tile = tileMap.GetTile(GetNearCellPosition(.05f));

        return tile;
    }

    private TileBase FindTileBack()
    {
        TileBase tile = tileMap.GetTile(GetNearCellPosition(.05f));

        return tile;
    }

    private TileBase FindTileLeft()
    {
        TileBase tile = tileMap.GetTile(GetNearCellPosition(.05f));

        return tile;
    }

    private TileBase FindTileRight()
    {
        TileBase tile = tileMap.GetTile(GetNearCellPosition(.05f));

        return tile;
    }

    public TileData FindTileData(TileBase tile)
    {
        return dataFromTiles[tile];
    }
}
