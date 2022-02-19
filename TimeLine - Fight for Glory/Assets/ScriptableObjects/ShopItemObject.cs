using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject/ShopItemObject")]
public class ShopItemObject : GenericItemObject
{
    private void Awake()
    {
        itemTypes.Add(ItemType.Shop);
    }
}
