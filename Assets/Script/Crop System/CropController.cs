using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropController : MonoBehaviour
{

    [SerializeField]
    private int currentItemIndex;

    [SerializeField]
    private InventorySO inventorySO;

    [SerializeField]
    private Tilemap decorationTileMap;

    [SerializeField]
    private InventoryItem inventoryItem;

    [SerializeField]
    private Crop crop;

    // Update is called once per frame
    void Update()
    {

        currentItemIndex = InventoryPage.Instance.currentSelectIndex;

        if(currentItemIndex != -1)
        {
            inventoryItem = inventorySO.GetItemAt(currentItemIndex);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 markedPosition = MarkerController.instance.GetMarkedPosition();
                Vector3Int markedCellPosition = MarkerController.instance.GetMarkedCellPosition();

                if (!CropManager.instance.isTileEffected(markedCellPosition))
                {
                     CropManager.instance.Plow(markedCellPosition);
                }
                else if (inventoryItem.Item is CropData)
                {
                    if(!CropManager.instance.plantedCrops.ContainsKey((Vector2)markedPosition))
                    {
                        crop.Plant((CropData)inventoryItem.Item);
                    }
                }
                else
                {
 
                    if (CropManager.instance.GetPlantedCrop(markedPosition))
                    {
                        CropManager.instance.GetPlantedCrop(markedPosition).Harvest();
                    }
                }
               
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                decorationTileMap.SetTile(MarkerController.instance.GetMarkedCellPosition(), null);
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                 

                
            }

        }

    }
}
