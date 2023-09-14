using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{

    public int CurrentDay;
    public int CurrentHour;
    public int CurrentMinute;

    public int TotalMoney;

    public CropData selectedCropToPlant;

    public Crop crop;

    //Singleton
    public static CropManager Instance;

    //Test
    public TileBase plowed;
    public TileBase seeded;
    public Tilemap effectTilemap;
    public Tilemap plantTilemap;

    public Dictionary<Vector2, CropData> plantedCrops;

    private void Start()
    {
        plantedCrops = new Dictionary<Vector2, CropData>();
    }

    public void Plow(Vector3Int position)
    {
        if (effectTilemap.GetTile(position) != null)
        {
            return;
        }

        CreatePlowedTile(position);

    }

    private void CreatePlowedTile(Vector3Int position)
    {
        effectTilemap.SetTile(position, plowed);
    }

    public bool isTileEffected(Vector3Int position)
    {
        Debug.Log("check");
        return effectTilemap.GetTile(position) != null;
    }

    public void Seed(Vector3 position, Crop crop)
    {
        this.selectedCropToPlant = crop.CurrentCrop;
        plantedCrops.Add((Vector2)position, selectedCropToPlant);
        CreatePlant(position, crop);
    }

    private void CreatePlant(Vector3 position, Crop crop)
    {
         Crop newCrop = Instantiate(crop, position, Quaternion.identity);

         newCrop.spriteRenderer.sprite = crop.CurrentCrop.growProgressSprites[0];
    }

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
