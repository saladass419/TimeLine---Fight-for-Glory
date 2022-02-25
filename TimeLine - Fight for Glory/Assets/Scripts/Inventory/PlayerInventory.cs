using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerInventory : GenericInventory
{
    private void Awake()
    {
        inventoryType = InventoryType.PlayerInventory;
        if(maxSlot==0) maxSlot = 24;
    }
    private void Start()
    {
        FindObjectOfType<Item>().itemPickUpEventInventory += AddItemToInventory;
        SetStartingItemsFromInspector();
    }
    
}
