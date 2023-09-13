using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    private CropData CurrentCrop;
    private int PlantDay;
    private int DaysSinceLastWatered;
    public SpriteRenderer spriteRenderer;

    public event Action<CropData> OnPlantCrop, OnHarvestCrop;

    // Returns the number of days that the crop has been planted for.
    int CropProgress()
    {
        return CropManager.Instance.CurrentDay - PlantDay;
    }
    // Can we currently harvest the crop?
    public bool CanHarvest()
    {
        return CropProgress() >= CurrentCrop.daysToGrow;
    }

    // Called when the crop has been planted for the first time.
    public void Plant(CropData crop)
    {
        CurrentCrop = crop;
        PlantDay = CropManager.Instance.CurrentDay;
        DaysSinceLastWatered = 1;
        UpdateCropSprite();
        OnPlantCrop?.Invoke(crop);
    }

    // Called when a new day ticks over.
    public void NewDayCheck()
    {
        DaysSinceLastWatered++;
        if (DaysSinceLastWatered > 3)
        {
            spriteRenderer.sprite = CurrentCrop.growProgressSprites[CurrentCrop.growProgressSprites.Length - 1];
        }
        UpdateCropSprite();
    }

    // Called when the crop has progressed.
    void UpdateCropSprite()
    {
        int cropProgress = CropProgress();
        if (cropProgress < CurrentCrop.daysToGrow)
        {
            spriteRenderer.sprite = CurrentCrop.growProgressSprites[cropProgress];
        }
        else
        {
            spriteRenderer.sprite = CurrentCrop.readyToHarvestSprite;
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
            OnHarvestCrop?.Invoke(CurrentCrop);
            Destroy(gameObject);
        }
    }


}
