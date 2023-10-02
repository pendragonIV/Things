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
    //public List<InventoryItem> initialItems = new List<InventoryItem>();

    // Start is called before the first frame update

    private void OnEnable()
    {
        PrepareInventoryData();

        PrepareUI();

        UpdateInventoryUI(playerInventory.GetItems());

        playerInventory.OnDataChange += UpdateInventoryUI;
    }

    private void Start()
    {
        if (!isInventoryDisplay)
        {
            inventoryPage.HideInventory();
        }

    }

    private void OnDisable()
    {
        playerInventory.OnDataChange -= UpdateInventoryUI;
    }

    public void PrepareInventoryData()
    {
        playerInventory.Initialize();

        //foreach(var item in initialItems)
        //{
        //    if(item.IsEmpty)
        //    {
        //        continue;
        //    }
        //    playerInventory.AddItem(item);
        //}

    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryData)
    {
        inventoryPage.ResetInventory();
        foreach (var item in inventoryData)
        {
            inventoryPage.UpdateItemData(item.Key, item.Value.Item.ItemImage);
            inventoryPage.ResetItemDescription();
        }
    }

    public void PrepareUI()
    {
        for (int i = 0; i < playerInventory.InventorySize; i++)
        {
            if(i >= playerInventory.InventorySize - 5)
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
        InventoryItem inventoryItem = playerInventory.GetItemAt(itemIndex);
        int quantity = inventoryItem.Quantity;
        if (inventoryItem.IsEmpty) { return; }
        ItemSO item = inventoryItem.Item;

        if(item is EffectableItemSO)
        {
            if((item as EffectableItemSO).isHealAble && GameManager.instance.player.GetComponent<Player>().unitHealth.CurrentHealth < GameManager.instance.player.GetComponent<Player>().unitHealth.CurrentMaxHealth)
            {
                GameManager.instance.player.GetComponent<Player>().unitHealth.Heal((item as EffectableItemSO).healAmount);
                playerInventory.ChangeQuantity(itemIndex, quantity - 1);
                if(quantity - 1 <= 0)
                {
                    playerInventory.DeleteItem(itemIndex);
                }
            }
        }
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
                inventoryPage.currentSelectIndex = -1;
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
