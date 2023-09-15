using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSpawnController : MonoBehaviour
{
    
    public static InventoryItemSpawnController instance;

    private void Awake()
    {
            instance = this;
    }

    [SerializeField]
    private CollectableItem itemSpawn;

    public void SpawnItem(InventoryItem item)
    {
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 1;
        CollectableItem collectableItem = Instantiate(itemSpawn,spawnPosition, Quaternion.identity);
        collectableItem.SetItem(item);
    }

}
