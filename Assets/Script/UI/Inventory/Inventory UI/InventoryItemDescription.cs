using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text itemName;
    [SerializeField]
    private TMP_Text itemDescription;
    [SerializeField]
    private Image itemBorder;
    [SerializeField]
    private Image quantityContainer;
    [SerializeField]
    private TMP_Text quantityText;

    private void Awake()
    {
        resetDescription();
    }

    public void resetDescription()
    {
        this.itemDescription.text = "";
        this.itemName.text = "";
        this.itemImage.sprite = null;
        this.itemDescription.enabled = false;
        this.itemName.enabled = false;
        this.itemImage.enabled = false;
        this.itemBorder.enabled = false;
        this.quantityContainer.enabled = false;
        this.quantityText.enabled = false;
    }

    public void setDescription(Sprite sprite, string name, string description, int quantity)
    {
        this.itemBorder.enabled = true;
        this.itemDescription.enabled = true;
        this.itemName.enabled = true;
        this.itemImage.enabled = true;
        this.quantityContainer.enabled = true;
        this.quantityText.enabled = true;
        this.quantityText.text = quantity.ToString();
        this.itemImage.sprite = sprite;
        this.itemName.text = name;
        this.itemDescription.text = description;
    }
}
