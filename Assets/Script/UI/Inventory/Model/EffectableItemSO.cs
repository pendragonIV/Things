using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectableItemSO", menuName = "Inventory/EffectableItemSO")]
public class EffectableItemSO : ItemSO
{
    [field: SerializeField]
    //public List<EffectSO> Effects { get; set; } = new List<EffectSO>();
    public bool isHealAble;
    [field: SerializeField]
    public int healAmount;
}
