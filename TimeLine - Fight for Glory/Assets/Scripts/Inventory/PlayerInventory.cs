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
        SetStartingEquipmentsFromInspector();
        FindObjectOfType<Item>().itemPickUpEventInventory += AddItemToInventory;
    }
    public void EquipItem(GenericItemObject item)
    {
        if (playerEquipment.Contains(item)) 
        {
            Debug.Log("Item already equipped!");
            return; 
        }

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