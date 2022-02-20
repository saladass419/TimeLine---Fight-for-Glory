using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerInventory : GenericInventory
{
    public List<GenericItemObject> playerEquipment = new List<GenericItemObject>();

    public event Action<AttributeName, float> itemEquipped;
    private void Awake()
    {
        inventoryType = InventoryType.PlayerInventory;
        if(maxSlot==0) maxSlot = 24;
    }
    private void Start()
    {
        SetStartingItemsFromInspector();
        FindObjectOfType<Item>().itemPickUpEventInventory += AddItemToInventory;

        UnequipItem(playerEquipment[0]);
    }
    public void EquipItem(GenericItemObject item)
    {
        if (playerEquipment.Contains(item)) return;

        RemoveItemFromInventory(item, 1);
        playerEquipment.Add(item);

        if (itemEquipped != null)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, attribute.Value); 
            }
        }
    }
    public void UnequipItem(GenericItemObject item)
    {
        if (itemEquipped != null)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, -1*attribute.Value);
            }
        }

        playerEquipment.Remove(item);
        AddItemToInventory(item, 1);
    }
}
