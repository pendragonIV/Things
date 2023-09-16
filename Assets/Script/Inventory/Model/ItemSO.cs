using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/ItemSO")]
public class ItemSO : ScriptableObject
{
    public int ItemID => GetInstanceID();
    [field: SerializeField]
    public bool IsStackable { get; set; }
    [field: SerializeField]
    public int MaxStack { get; set; } = 1;

    [field: SerializeField]
    public string ItemName { get; set;}
    [field: SerializeField]
    [field: TextArea]
    public string ItemDescription { get; set; }
    [field: SerializeField]
    public Sprite ItemImage { get; set; }
    [field: SerializeField]
    public int purchasePrice;
    [field: SerializeField]
    public int sellPrice;

}
