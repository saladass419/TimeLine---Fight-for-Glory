using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemObject : GenericItemObject
{
    private void OnValidate()
    {
        if (!itemTypes.Contains(ItemType.Equipment))
            itemTypes.Add(ItemType.Equipment);
    }
}
public enum EquipmentType
{
    None, Helmet,Chestplate, Leggings, Shoes, Shield, Sword
}
