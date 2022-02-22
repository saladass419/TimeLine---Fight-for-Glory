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
        FindObjectOfType<Item>().itemPickUpEventInventory += AddItemToInventory;
        SetStartingItemsFromInspector();
        SetStartingEquipmentsFromInspector();
    }
    public void EquipItem(GenericItemObject item)
    {
        if (playerEquipment.Contains(item)) 
        {
            Debug.Log("Item already equipped!");
            return; 
        }

        if (!RemoveItemFromInventory(item, 1)) return;
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
        if (!AddItemToInventory(item, 1)) return;
        playerEquipment.Remove(item);
        if (itemEquipped != null)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, -1*attribute.Value);
            }
        }
    }
    private void SetStartingEquipmentsFromInspector()
    {
        foreach (GenericItemObject item in playerEquipment)
        {
            foreach (Attribute attribute in item.Attributes)
            {
                itemEquipped(attribute.AttributeName, attribute.Value);
            }
        }
    }
}
