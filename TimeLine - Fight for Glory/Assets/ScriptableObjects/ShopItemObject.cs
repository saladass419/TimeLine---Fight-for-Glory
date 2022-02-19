using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject/ShopItemObject")]
public class ShopItemObject : GenericItemObject
{
    private void Awake()
    {
        if(!itemTypes.Contains(ItemType.Shop))
            itemTypes.Add(ItemType.Shop);
    }
}
