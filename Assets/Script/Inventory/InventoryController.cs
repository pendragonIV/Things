using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryController : MonoBehaviour
{

    [SerializeField]
    private InventoryPage inventoryPage;

    [SerializeField]
    private InventorySO playerInventory;

    private bool isInventoryDisplay = false;

    //Test
    public List<InventoryItem> initialItems = new List<InventoryItem>();

    // Start is called before the first frame update
    void Start()
    {

        inventoryPage.HideInventory();

        PrepareInventoryData();

        PrepareUI();
    }

    public void PrepareInventoryData()
    {
        playerInventory.Initialize();

        playerInventory.OnDataChange += UpdateInventoryUI;

        foreach(var item in initialItems)
        {
            if(item.IsEmpty)
            {
                continue;
            }
            playerInventory.AddItem(item);
        }

    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryData)
    {
        inventoryPage.ResetInventory();
        foreach (var item in inventoryData)
        {
            inventoryPage.UpdateItemData(item.Key, item.Value.Item.ItemImage);
        }
    }

    public void PrepareUI()
    {
        for (int i = 0; i < playerInventory.InventorySize; i++)
        {
            if(i >= playerInventory.InventorySize - 9)
            {
                inventoryPage.AddItemHolderToolBar();
            }
            else
            {
                inventoryPage.AddItemHolderInventory();
            }  
        }

        #region Set Events

        this.inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        this.inventoryPage.OnStartDrag += HandleStartDrag;
        this.inventoryPage.OnSwapItems += HandleSwapItems;
        this.inventoryPage.OnEndDrag += HandleEndDrag;

        #endregion

    }


    #region Action Handlers
    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = playerInventory.GetItems()[itemIndex];
        if (inventoryItem.IsEmpty) {return;}
      
        ItemSO item = inventoryItem.Item;
        inventoryPage.UpdateItemDescription(itemIndex,item.ItemName, item.ItemDescription, inventoryItem.Quantity, item.ItemImage);
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        
    }

    private void HandleStartDrag(int itemIndex)
    {
        InventoryItem inventoryItem = playerInventory.GetItems()[itemIndex];
        if (inventoryItem.IsEmpty)
            return;
        ItemSO item = inventoryItem.Item;
        inventoryPage.CreateDraggableItem(item.ItemImage);
    }

    private void HandleEndDrag(int currentDragIndex)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            InventoryItem spawnItem = playerInventory.GetItemAt(currentDragIndex);

            InventoryItemSpawnController.instance.SpawnItem(spawnItem);

            playerInventory.DeleteItem(currentDragIndex);
            inventoryPage.ResetItemDescription();
        }
        inventoryPage.DestroyDraggableItem();
    }
    private void HandleSwapItems(int itemIndex, int itemSwapIndex)
    {
        playerInventory.SwapItems(itemIndex, itemSwapIndex);
    }

    #endregion

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!isInventoryDisplay)
            {
                inventoryPage.ShowInventory();
                isInventoryDisplay = true;
                foreach(var item in playerInventory.GetItems())
                {
                    inventoryPage.UpdateItemData(item.Key, item.Value.Item.ItemImage);
                }
                
            }
            else
            {
                inventoryPage.HideInventory();
                isInventoryDisplay = false;
                
            }
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameManager.instance.player.GetComponent<Player>().isPlayerCanAttack = false;
        }
        else
        {
            GameManager.instance.player.GetComponent<Player>().isPlayerCanAttack = true;
        }


    }
}
