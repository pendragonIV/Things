using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSpawnController : MonoBehaviour
{
    
    public static InventoryItemSpawnController instance;

    private void Awake()
    {
            instance = this;
            Debug.Log("InventoryItemSpawnController instance created");
    }

    [SerializeField]
    private CollectableItem itemSpawn;

    public void SpawnItem(InventoryItem item)
    {
        CollectableItem collectableItem = Instantiate(itemSpawn, Camera.main.ScreenToViewportPoint(Input.mousePosition), Quaternion.identity);
        collectableItem.SetItem(item);
    }

}
