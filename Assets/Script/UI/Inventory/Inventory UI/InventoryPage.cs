using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPage : MonoBehaviour
{

    #region Components

    [SerializeField]
    private RectTransform itemHolderContainer;
    [SerializeField] 
    private RectTransform toolBar;

    [SerializeField]
    private InventoryItemHolder inventoryItemHolder;

    [SerializeField]
    private InventoryItemDescription inventoryItemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;

    private int currentDragIndex = -1;
    public int currentSelectIndex = -1;

    #endregion

    public static InventoryPage Instance;

    private List<InventoryItemHolder> itemHolders = new List<InventoryItemHolder>();

    #region Events
    // int is the index of the item in the inventory

    public event Action<int> OnDescriptionRequested,
                       OnItemActionRequested,
                       OnStartDrag,
                       OnEndDrag;

    public event Action<int, int> OnSwapItems;

    #endregion

    private void Awake()
    {
        mouseFollower.Toggle(false);

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void AddItemHolderInventory()
    {
        InventoryItemHolder holder = Instantiate(inventoryItemHolder, Vector3.zero, Quaternion.identity) as InventoryItemHolder;

        holder.transform.SetParent(itemHolderContainer.transform);

        holder.OnItemClicked += HandleItemSelection;
        holder.OnItemDroppedOn += HandleSwap; 
        holder.OnItemBeginDrag += HandleBeginDrag;
        holder.OnItemEndDrag += HandleEndDrag;
        holder.OnMouseButtonClick += HandleShowItemActions;


        itemHolders.Add(holder);
    }

    public void AddItemHolderToolBar()
    {
        InventoryItemHolder holder = Instantiate(inventoryItemHolder, Vector3.zero, Quaternion.identity) as InventoryItemHolder;

        holder.transform.SetParent(toolBar.transform);

        holder.OnItemClicked += HandleItemSelection;
        holder.OnItemDroppedOn += HandleSwap;
        holder.OnItemBeginDrag += HandleBeginDrag;
        holder.OnItemEndDrag += HandleEndDrag;
        holder.OnMouseButtonClick += HandleShowItemActions;


        itemHolders.Add(holder);
    }

    #region Display Inventory
    public void ShowInventory()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void HideInventory()
    {
        gameObject.SetActive(false);
        DestroyDraggableItem();
    }
    #endregion

    #region Support Functions

    public void UpdateItemData(int itemIndex, Sprite itemImage)
    {
        if (itemIndex < itemHolders.Count)
        {
            itemHolders[itemIndex].setData(itemImage);
        }
    }

    public void UpdateItemDescription(int itemIndex, string itemName, string itemDescription, int quantity, Sprite itemImage)
    {
        if (itemIndex < itemHolders.Count)
        {
            inventoryItemDescription.setDescription(itemImage, itemName, itemDescription,quantity);
        }
    }

    public void ResetItemDescription()
    {
        inventoryItemDescription.resetDescription();
    }

    internal void ResetInventory()
    {
        foreach(var holder in itemHolders)
        {
            holder.deSelect();
            holder.toEmpty();
        }
    }

    public void CreateDraggableItem(Sprite sprite)
    {
        mouseFollower.Toggle(true);
        mouseFollower.setData(sprite);
    }

    public void DestroyDraggableItem() 
    {

        mouseFollower.Toggle(false);
        currentDragIndex = -1;

    }

    public void ResetSelection()
    {
        inventoryItemDescription.resetDescription();
        DeselectAllItem();
    }

    private void DeselectAllItem()
    {
        for (int i = 0; i < itemHolders.Count; i++)
        {
            itemHolders[i].deSelect();
        }
    }

    #endregion

    #region Action Handlers for each event

    private void HandleShowItemActions(InventoryItemHolder holder)
    {
        
    }

    private void HandleBeginDrag(InventoryItemHolder holder)
    {
        int holderIndex = itemHolders.IndexOf(holder);
        if (holderIndex == -1)
        {
            return;
        }
        currentDragIndex = itemHolders.IndexOf(holder);
        HandleItemSelection(holder);
        OnStartDrag?.Invoke(currentDragIndex);

    }

    private void HandleEndDrag(InventoryItemHolder holder)
    {
        OnEndDrag?.Invoke(currentDragIndex);
    }

    private void HandleSwap(InventoryItemHolder holder)
    {
        int holderIndex = itemHolders.IndexOf(holder);
        if(holderIndex == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentDragIndex, holderIndex);
    }

    private void HandleItemSelection(InventoryItemHolder holder)
    {
        if(holder.getItemSprite() == null)
        {
            DeselectAllItem();
            currentSelectIndex = -1;
            inventoryItemDescription.resetDescription();
            return;
        }
        else
        {
            holder.select();
            for (int i = 0; i < itemHolders.Count; i++)
            {
                if (itemHolders[i] != holder)
                {
                    itemHolders[i].deSelect();
                }
            }
        }
        currentSelectIndex = itemHolders.IndexOf(holder);
        OnDescriptionRequested?.Invoke(currentSelectIndex);

    }

    #endregion

}
