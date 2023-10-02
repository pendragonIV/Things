using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public TileBase plowed;
    public Tilemap effectTilemap;

    public Dictionary<Vector2, Crop> plantedCrops;
    public Dictionary<CropData, ItemSO> CropAndSpawnItemPair;
    // Singleton
    public static CropManager instance;
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        CurrentDay = DayNightController.instance.days;
        CurrentHour = (int)DayNightController.instance.hour;
        CurrentMinute = (int)DayNightController.instance.minute;
    }

    private void Start()
    {
        plantedCrops = new Dictionary<Vector2, Crop>();
        CropAndSpawnItemPair = new Dictionary<CropData, ItemSO>();
    }

    #region Support Methods

    public bool isTileEffected(Vector3Int position)
    {
        return effectTilemap.GetTile(position) != null;
    }

    private void CreatePlant(Vector3 cropPosition, Crop crop)
    {
        Crop newCrop = Instantiate(crop, cropPosition, Quaternion.identity);
        plantedCrops.Add((Vector2)cropPosition, newCrop);
        newCrop.spriteRenderer.sprite = crop.CurrentCrop.growProgressSprites[0];
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

    public Crop GetPlantedCrop(Vector3 position)
    {
        if (plantedCrops.ContainsKey((Vector2)position))
        {
            return plantedCrops[position];
        }
        return null;
    }

    #endregion

    private void OnEnable()
    {
        this.crop.OnPlantCrop += OnPlantCrop;
    }

    private void OnDisable()
    {
        this.crop.OnPlantCrop -= OnPlantCrop;
    }

    // Called when a crop has been planted.
    // Listening to the Crop.onPlantCrop event.
    public void OnPlantCrop(CropData Plantcrop, Vector3 cropPosition)
    {
        this.selectedCropToPlant = Plantcrop;

        if(!CropAndSpawnItemPair.ContainsKey(Plantcrop))
        {
            ItemSO newSpawnCrop = createSpawnCrop();
            CropAndSpawnItemPair.Add(Plantcrop, newSpawnCrop);
        }

        CreatePlant(cropPosition, crop);

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

    private ItemSO createSpawnCrop()
    {
        ItemSO newSpawnCrop = ScriptableObject.CreateInstance<ItemSO>();

        newSpawnCrop.ItemName = null;
        newSpawnCrop.ItemDescription = null;
        newSpawnCrop.IsStackable = false;
        newSpawnCrop.MaxStack = 0;
        newSpawnCrop.purchasePrice = 0;
        newSpawnCrop.sellPrice = 0;
        newSpawnCrop.ItemImage = null;

        return newSpawnCrop;
    }

    public void SetSpawnItem(CropData cropData, string name, string description,bool isStackable,
        int maxStack, int purchasePrice, int sellPrice, Sprite ItemImage)
    {
       ItemSO spawnCrop = CropAndSpawnItemPair[cropData];
       spawnCrop.ItemName = name;
       spawnCrop.ItemDescription = description;
       spawnCrop.IsStackable = isStackable;
       spawnCrop.MaxStack = maxStack;
       spawnCrop.purchasePrice = purchasePrice;
       spawnCrop.sellPrice = sellPrice;
       spawnCrop.ItemImage = ItemImage;
    }

    public void UnsetSpawnItem(CropData cropData)
    {
       ItemSO spawnCrop = CropAndSpawnItemPair[cropData];
       spawnCrop.ItemName = null;
       spawnCrop.ItemDescription = null;
       spawnCrop.IsStackable = false;
       spawnCrop.MaxStack = 0;
       spawnCrop.purchasePrice = 0;
       spawnCrop.sellPrice = 0;
       spawnCrop.ItemImage = null;
    }

}
