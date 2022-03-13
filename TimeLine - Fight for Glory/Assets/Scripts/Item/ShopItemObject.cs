using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject/ShopItemObject")]
public class ShopItemObject : GenericItemObject
{
    private void OnValidate()
    {
        if (!ItemTypes.Contains(ItemType.Shop))
            ItemTypes.Add(ItemType.Shop);
    }
}
