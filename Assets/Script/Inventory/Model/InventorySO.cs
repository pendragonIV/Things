using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.Linq;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Inventory/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> items;

    [field: SerializeField]
    public int InventorySize { get; private set; } = 20;

    public event Action<Dictionary<int, InventoryItem>> OnDataChange;

    public void Initialize()
    {
        items = new List<InventoryItem>();
        for (int i = 0; i < InventorySize; i++)
        {
            items.Add(InventoryItem.GetEmptyItem());
        }
    }

    private bool IsInventoryIsFull()
    {
        //return items.Count == InventorySize;
        return items.Where(item => item.IsEmpty).Any() == false;
    }

    public int AddItem(ItemSO item, int quantity)
    {
        //In this function, quantity is returned is the item's remaining quantity

        if (item == null || quantity == 0)
        {
            Debug.LogError("Item is null or quantity = 0");
            return quantity;
        }
        if (IsInventoryIsFull())
        {
            return quantity;
        }
        if(!item.IsStackable)
        {
            quantity -= AddNonStackableItem(item, 1);
            InfromDataChange();
     
            return quantity;
        }

        //This will add stackable items and return the remaining quantity (That means the stakable item is reached the max quantity)
        quantity = AddStackableItem(item, quantity);
        InfromDataChange();
        return quantity;

    }

    private int AddNonStackableItem(ItemSO item, int quantity)
    {
        InventoryItem newNonstackableItem = new InventoryItem
        {
            Item = item,
            Quantity = quantity
        };

        //Find the first empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].IsEmpty)
            {
                items[i] = newNonstackableItem;
                return quantity;
            }
        }
        //Just in case there is no empty slot
        return 0;
    }

    private int AddStackableItem(ItemSO item, int quantity)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Item == item)
            {
                int spaceLeft = item.MaxStack - items[i].Quantity;
                if (spaceLeft >= quantity)
                {
                    items[i] = items[i].ChangeQuantity(items[i].Quantity + quantity);
                    return 0; //This means the item is added successfully and there is no remaining items
                }
                else
                {
                    items[i] = items[i].ChangeQuantity(items[i].Quantity + spaceLeft);
                    quantity -= spaceLeft;
                }
            }
        }
        while (quantity > 0 && !IsInventoryIsFull()) //If there is remaining items and the inventory is not full, add new stackable item
        {
            //Read the doc about Clamp if u forget (Just to remind myself)
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStack);
            quantity -= AddNewStackableItem(item, newQuantity);
        }

        return quantity; //Return remaining quantity
    }

    public int AddNewStackableItem(ItemSO item, int quantity)
    {
        InventoryItem newStackableItem = new InventoryItem
        {
            Item = item,
            Quantity = quantity
        };

        //Find the first empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].IsEmpty)
            {
                items[i] = newStackableItem;
                return quantity;
            }
        }
        //Just in case there is no empty slot
        return 0;
    }

    public void AddItem(InventoryItem item)
    {
        AddItem(item.Item, item.Quantity);
    }

    public Dictionary<int, InventoryItem> GetItems()
    {
        Dictionary<int, InventoryItem> returnItems = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < items.Count; i++)
        {
            if (this.items[i].IsEmpty)
                continue;
            returnItems[i] = this.items[i];
        }
        return returnItems;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return items[itemIndex];
    }

    internal void SwapItems(int itemIndex, int itemSwapIndex)
    {
        InventoryItem temp = items[itemIndex];
        items[itemIndex] = items[itemSwapIndex];
        items[itemSwapIndex] = temp;

        InfromDataChange();
    }

    private void InfromDataChange()
    {
        OnDataChange?.Invoke(GetItems());
    }
}

[Serializable]
public struct InventoryItem
{
    public ItemSO Item;
    public int Quantity;
    public bool IsEmpty => Item == null;

    public  InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            Item = this.Item,
            Quantity = newQuantity
        };
    }

    public static InventoryItem GetEmptyItem() => new InventoryItem
    {
        Item = null,
        Quantity = 0
    };
}
