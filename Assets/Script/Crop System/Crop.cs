using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    [SerializeField]
    public CropData CurrentCrop;
    [SerializeField]
    private int PlantDay;
    [SerializeField]
    private int PlantHour;
    [SerializeField]
    private int PlantMinute;
    [SerializeField]
    private int DaysSinceLastWatered;
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private Vector3 cropPosition;

    [SerializeField]
    private CollectableItem collectableCrop;

    [SerializeField]
    private InventorySO inventorySO;


    public event Action<CropData,Vector3> OnPlantCrop;

    private const float DAY_LENGTH = 86400f;
    private const float HOUR_LENGTH = 3600f;
    private const float MINUTE_LENGTH = 60f;


    private void Update()
    {
        if (CurrentCrop != null && CropProgress() <= 100)
        {
            UpdateCropSprite();
        }
    }

    // Returns the number of days that the crop has been planted for.
    public int CropProgress()
    {
        return ProgressPercent();
    }

    private int ProgressPercent()
    {
        int currentDay = CropManager.instance.CurrentDay;
        int currentHour = CropManager.instance.CurrentHour;
        int currentMinute = CropManager.instance.CurrentMinute;

        int days = currentDay - PlantDay;
        int hours = currentHour - PlantHour;
        int minutes = currentMinute - PlantMinute;

        float totalTime = (days * DAY_LENGTH ) + (hours * HOUR_LENGTH) + (minutes * MINUTE_LENGTH);

        float havestTime = CurrentCrop.daysToGrow * DAY_LENGTH;

        

        return Mathf.RoundToInt((totalTime / havestTime) * 100);
    }

    private int CropStateGap()
    {
        int totalProgressState = CurrentCrop.growProgressSprites.Length;
        return 100 / totalProgressState;
    }

    // Can we currently harvest the crop?
    public bool CanHarvest()
    {
        return CropProgress() >= 100;
    }


    // Called when the crop has been planted for the first time.
    public void Plant(CropData crop)
    {
        CurrentCrop = crop;
        PlantDay = CropManager.instance.CurrentDay;
        PlantHour = CropManager.instance.CurrentHour;
        PlantMinute = CropManager.instance.CurrentMinute;
        DaysSinceLastWatered = 1;
        cropPosition = MarkerController.instance.GetMarkedPosition();
        cropPosition.z = 10;
        UpdateCropSprite();
        OnPlantCrop?.Invoke(crop, cropPosition);
    }

    // Called when a new day ticks over.
    public void NewDayCheck()
    {
        DaysSinceLastWatered++;
        if (DaysSinceLastWatered > 3)
        {
            spriteRenderer.sprite = CurrentCrop.deadCropSprite;
        }
    }

    // Called when the crop has progressed.
    void UpdateCropSprite()
    {
        int currentState = CropProgress() / CropStateGap();
        if(CropProgress() < 100)
        {
            spriteRenderer.sprite = CurrentCrop.growProgressSprites[currentState];
        }
        if (CropProgress() >= 100)
        {
            spriteRenderer.sprite = CurrentCrop.readyToHarvestSprite;
            CurrentCrop.isAlreadyToHarvest = true;
        }
    }

    // Called when the crop has been watered.
    public void Water()
    {
        DaysSinceLastWatered = 0;
    }
    // Called when we want to harvest the crop.
    public void Harvest()
    {
        if (CanHarvest())
        {
            if (CropManager.instance.CropAndSpawnItemPair.ContainsKey(CurrentCrop))
            {
                CropManager.instance.SetSpawnItem(CurrentCrop, CurrentCrop.ItemName, CurrentCrop.ItemDescription, CurrentCrop.IsStackable, CurrentCrop.MaxStack
                    , CurrentCrop.purchasePrice, CurrentCrop.sellPrice, CurrentCrop.itemSpawnSprite);

                ItemSO itemSpawn = CropManager.instance.CropAndSpawnItemPair[CurrentCrop];
                InventoryItem item = new InventoryItem
                {
                    Item = itemSpawn,
                    Quantity = CurrentCrop.harvestQuantity
                };



                CollectableItem collectableItem = Instantiate(collectableCrop, cropPosition, Quaternion.identity);
                
                collectableItem.SetItem(item);

                StartCoroutine(WaitForPickup());

                collectableItem.boxCollider2D.enabled = true;

                Destroy(gameObject);
            }

        }
    }

    private IEnumerator WaitForPickup()
    {
        yield return new WaitForSeconds(2);
    }

}
