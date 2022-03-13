using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemObject : GenericItemObject
{
    private void OnValidate()
    {
        if (!ItemTypes.Contains(ItemType.Equipment))
            ItemTypes.Add(ItemType.Equipment);
    }
}
public enum EquipmentType
{
    None, Helmet,Chestplate, Leggings, Shoes, Sword, Shield
}
