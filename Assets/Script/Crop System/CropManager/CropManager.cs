using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{

    public int CurrentDay;
    public int CurrentHour;
    public int CurrentMinute;

    public int CurrentSecond;

    public int TotalMoney;

    public CropData selectedCropToPlant;

    public Crop crop;

    //Singleton
    public static CropManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
    }

    private void OnEnable()
    {
        this.crop.OnPlantCrop += OnPlantCrop;
        this.crop.OnHarvestCrop += OnHarvestCrop;
    }

    private void OnDisable()
    {
        this.crop.OnPlantCrop -= OnPlantCrop;
        this.crop.OnHarvestCrop -= OnHarvestCrop;
    }

    // Called when a crop has been planted.
    // Listening to the Crop.onPlantCrop event.
    public void OnPlantCrop(CropData cop)
    {
        
    }

    // Called when a crop has been harvested.
    // Listening to the Crop.onCropHarvest event.
    public void OnHarvestCrop(CropData crop)
    {
    }

    // Called when we want to purchase a crop.
    public void PurchaseCrop(CropData crop)
    {
    }

    // Do we have enough crops to plant?
    public bool CanPlantCrop()
    {
        return false;
    }

    // Called when the buy crop button is pressed.
    public void OnBuyCropButton(CropData crop)
    {
    }

}
