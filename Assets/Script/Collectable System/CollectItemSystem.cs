using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemSystem : MonoBehaviour
{

    [SerializeField]
    private InventorySO inventorySO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectableItem collectableItem = collision.GetComponent<CollectableItem>();
        if(collectableItem != null)
        {
            int remainingQuantity = inventorySO.AddItem(collectableItem.InventoryItem, collectableItem.Quantity);
            if(remainingQuantity == 0)
            {
                collectableItem.DestroyItem();
            }
            else
            {
                collectableItem.Quantity = remainingQuantity;
            }
        }
    }

}
