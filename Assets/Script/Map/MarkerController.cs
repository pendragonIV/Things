using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerController : MonoBehaviour
{

    [SerializeField]
    private Tilemap markerTileMap;

    [SerializeField]
    private TileBase markerTile;

    [SerializeField]
    private Vector3Int markerPosition;

    [SerializeField]
    private Vector3Int oldMarkerPosition;

    [SerializeField]
    private TileMapReader tileMapReader;

    private TileData tileData;

    public static MarkerController instance;

    private void Awake()
    {
        instance = this;
    }

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

    }

    public void Marker()
    {
        Vector3Int CellPosition = tileMapReader.GetNearCellPosition(.05f);
        markerPosition = CellPosition;
    }

    public Vector3 GetMarkedPosition()
    {
        Vector3 newCropPosition = tileMapReader.GetNearPosition(markerPosition);

        //tile size is 16*16 so we need to add 8 to get the center of the tile
        newCropPosition.y += .08f;
        newCropPosition.x += .08f;

        return newCropPosition;
    }
    public Vector3Int GetMarkedCellPosition()
    {
        return markerPosition;
    }


}
