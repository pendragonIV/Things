using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerController : MonoBehaviour
{

    [SerializeField]
    private Tilemap markerTileMap;

    [SerializeField]
    private Tilemap decorationTileMap;

    [SerializeField]
    private TileBase markerTile;

    [SerializeField]
    private Vector3Int markerPosition;

    [SerializeField]
    private Vector3Int oldMarkerPosition;

    [SerializeField]
    private TileMapReader tileMapReader;

    private TileData tileData;

    private void Start()
    {
        tileMapReader = GetComponent<TileMapReader>();
    }

    private void Update()
    {
        markerTileMap.SetTile(oldMarkerPosition, null);
        if (tileMapReader.FindNearTile())
        {
            tileData = tileMapReader.dataFromTiles[tileMapReader.FindNearTile()];
            if (tileData.isPlowable)
            {
                Marker();
                markerTileMap.SetTile(markerPosition, markerTile);
                oldMarkerPosition = markerPosition;
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            decorationTileMap.SetTile(markerPosition, null);
        }


    }

    public void Marker()
    {
        Vector3Int CellPosition = tileMapReader.GetNearCellPosition(.05f);
        markerPosition = CellPosition;
    }



}
