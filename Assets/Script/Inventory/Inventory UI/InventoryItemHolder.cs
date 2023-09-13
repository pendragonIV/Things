using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemHolder : MonoBehaviour, IBeginDragHandler, IDropHandler, IEndDragHandler, IPointerClickHandler, IDragHandler
{

    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text itemAmountText;
    [SerializeField]
    private Image itemSelectedBorder;

    public Action<InventoryItemHolder> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnMouseButtonClick;

    private bool isItemInSlot = false; 

    private void Awake()
    {
        deSelect();
    }

    public Sprite getItemSprite()
    {
        return itemImage.sprite;
    }

    public void toEmpty()
    {
        this.itemImage.sprite = null;
        this.itemImage.gameObject.SetActive(false);
        isItemInSlot = false;
    }

    public void setData(Sprite sprite)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        isItemInSlot = true;
    }

    public void deSelect()
    {
        itemSelectedBorder.enabled = false;
    }

    public void select()
    {
        itemSelectedBorder.enabled = true;
    }

    #region Events

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isItemInSlot)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    #endregion


}
