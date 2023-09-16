using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CropData", menuName = "Crop System/CropData")]
public class CropData : ItemSO
{
    [field: SerializeField]
    public int daysToGrow;
    [field: SerializeField]
    public Sprite[] growProgressSprites;
    [field: SerializeField]
    public Sprite readyToHarvestSprite;
    [field: SerializeField]
    public Sprite deadCropSprite;
    [field: SerializeField]
    public Sprite itemSpawnSprite;
    [field: SerializeField]
    public int harvestQuantity;
    [field: SerializeField]
    public bool isAlreadyToHarvest = false;

}
